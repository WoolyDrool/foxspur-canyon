using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsAnimator : MonoBehaviour
{
    //Makes the clouds gradually change from a random state somewhere between state 1 and 2, to another random state between state 1 and 2, over a period of time between min- and maxChangeDuration.

    //state1:
    public float CloudsTop1;
    public float CloudsHorizon1;
    public float Thickness1;
    public Color Light1 = Color.white;
    public Color Dark1 = Color.gray;
    public Color FogColor1 = Color.white;
    public float FogIntensity1 = 0.01f;
    public Color SceneFogColor1 = Color.white;
    public float FadeOut1 = 0.25f;

    //state2:
    public float CloudsTop2;
    public float CloudsHorizon2;
    public float Thickness2;
    public Color Light2 = Color.white;
    public Color Dark2 = Color.gray;
    public Color FogColor2 = Color.white;
    public float FogIntensity2 = 0.01f;
    public Color SceneFogColor2 = Color.white;
    public float FadeOut2 = 0.25f;

    public float minChangeDuration = 10;
    public float maxChangeDuration = 100;

    private float duration;
    private float progress;
    private float startFactor;
    private float endFactor;

    public CloudsController cloudsController;

    void Start()
    {
        endFactor = Random.value;
        //Debug.Log("Start value: " + endFactor);
        SetNewTarget();
    }

    private void SetNewTarget()
    {
        duration = Random.Range(minChangeDuration, maxChangeDuration);
        startFactor = endFactor;
        endFactor = Random.value;
        //Debug.Log("target value: "+ endFactor);
        progress = 0;
        UpdateClouds(startFactor);
    }

    void Update()
    {
        progress += Time.deltaTime / duration;

        if (progress < 1)
        {
            float curvedprogress = SCurve(progress);
            float factor = Mathf.Lerp(startFactor, endFactor, curvedprogress);
            UpdateClouds(factor);
        }
        else
        {
            SetNewTarget();
        }
    }

    private float SCurve(float progress) //adds ease-in and ease-out
    {
        float factorL = progress * progress;
        float factorH = 1 - progress;
        factorH *= factorH;
        factorH = 1 - factorH;
        float curvedprogress = factorL * (1 - progress) + factorH * progress;
        return curvedprogress;
    }

    private void UpdateClouds(float factor)
    {
        cloudsController.SetClouds(Mathf.Lerp(CloudsTop1, CloudsTop2, factor));
        cloudsController.SetCloudsHorizon(Mathf.Lerp(CloudsHorizon1, CloudsHorizon2, factor));
        cloudsController.SetThickness(Mathf.Lerp(Thickness1, Thickness2, factor));
        cloudsController.SetLightColor(Color.Lerp(Light1, Light2, factor));
        cloudsController.SetDarkColor(Color.Lerp(Dark1, Dark2, factor));
        cloudsController.SetFogColor(Color.Lerp(FogColor1, FogColor2, factor));
        cloudsController.SetFogIntensity(Mathf.Lerp(FogIntensity1, FogIntensity2, factor));
        cloudsController.SetFadeOut(Mathf.Lerp(FadeOut1, FadeOut2, factor));
        RenderSettings.fogColor = Color.Lerp(SceneFogColor1, SceneFogColor2, factor);
    }
}
