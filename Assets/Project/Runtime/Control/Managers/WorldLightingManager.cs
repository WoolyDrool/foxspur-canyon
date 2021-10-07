using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class WorldLightingManager : MonoBehaviour
{
    [SerializeField] private Light Sun;
    [SerializeField] private WorldLightingPreset preset;
    [SerializeField] private float TimeOfDay;
    [SerializeField, Range(0, 24)] private float inspectorTimeVisualization;
    private Vector3 adjustedSunPosition;
    public bool updateSun = true;
    [SerializeField] private float tickRate = 10;
    public TimeManager timeManager;
    public bool useStartTime = true;
    public float stopTime;
    public bool useStopTime = false;
    private void Start()
    {
        if(useStartTime)
        {
        }
        TimeOfDay = timeManager.startTime;
        
        GetInitialLighting(timeManager.currentTime / timeManager.dayLength);
    }

    private void Update()
    {
        if(preset == null)
            return;

        if (Application.isPlaying)
        {
            if (updateSun)
            {
                TimeOfDay = timeManager.currentTime;
                UpdateLighting(timeManager.currentTime / timeManager.dayLength);

                if (useStopTime)
                {
                    if (TimeOfDay >= stopTime)
                    {
                        updateSun = false;
                    }
                }
            }
        }
        else
        {
            UpdateLighting(inspectorTimeVisualization / 24);
        }
    }

    private void GetInitialLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        //RenderSettings.fogDensity = preset.FogDensity.Evaluate(timePercent);
        //RenderSettings.ambientIntensity = preset.AtmosphereIntensity.Evaluate(timePercent);
        
        if (Sun != null)
        {
            Sun.color = preset.SunColor.Evaluate(timePercent);
            adjustedSunPosition = new Vector3((timePercent * 360f) - 90f, 170, 0);
            Sun.transform.localRotation = Quaternion.Euler(adjustedSunPosition);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        //RenderSettings.fogDensity = preset.FogDensity.Evaluate(timePercent);
        //RenderSettings.ambientIntensity = preset.AtmosphereIntensity.Evaluate(timePercent);


        if (Sun != null)
        {
            Sun.color = preset.SunColor.Evaluate(timePercent);
            Sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));

        }
    }

    private void OnValidate()
    {
        if(Sun != null)
            return;
        if (RenderSettings.sun != null)
        {
            Sun = RenderSettings.sun;
        }
    }
    
}
