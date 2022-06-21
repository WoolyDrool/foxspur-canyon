using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SunController : MonoBehaviour
{
    public Material cloudsmat;
    public float shadowFactor = 0.5f;

    private Light sun;
    private Texture2D cloudsTexture;
    private float cloudsAmount;
    private float cloudsHorizonAmount;
    private float cloudThickness;

    void Start()
    {
        sun = GetComponent<Light>();
        cloudsTexture = cloudsmat.GetTexture("_MainTex") as Texture2D;
    }

    void Update()
    {
        var sunpos = cloudsmat.GetVector("_SunDir");
        cloudsAmount = cloudsmat.GetFloat("_Clouds");
        cloudsHorizonAmount = cloudsmat.GetFloat("_HorizonClouds");
        cloudThickness = cloudsmat.GetFloat("_CloudThickness");
        sun.intensity = GetLightIntensity(new Vector2(sunpos.x, sunpos.y));
    }

    private float GetLightIntensity(Vector2 SunPos)
    {
        Vector2 distancexy = (SunPos - new Vector2(0.5f, 0.5f)) * 2;
        float distance = Mathf.Clamp01(distancexy.magnitude);
        float cloudfactor = cloudsAmount * (1 - distance) + cloudsHorizonAmount * distance;
        //Debug.Log("cloudfactor1: " + cloudfactor);
        //Vector3 cloudtex = tex2D(_MainTex, i.uv);
        var sample = cloudsTexture.GetPixelBilinear(SunPos.x, SunPos.y);
        float factor = 1;// (Mathf.Sin(Time.time / 8.0f) + 1) * 0.5f;//(_SinTime.x + 1) * 0.5;
        float clouds = sample.r * factor + sample.g * (1 - factor); //shape change
        cloudfactor = Mathf.Clamp01((clouds - (1 - cloudfactor))* cloudThickness);
        Debug.Log("cloudfactor2: "+ cloudfactor);
        return 1 - cloudfactor * shadowFactor;
    }
}
