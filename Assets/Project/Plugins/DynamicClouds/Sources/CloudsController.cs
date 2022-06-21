using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class CloudsController : MonoBehaviour
{
    public Light sun;
    public bool CloudsDimSunlight = true;
    public float shadowFactor = 0.5f;
    private float MaxSunIntensity;
    public Vector2 WindSpeed = new Vector2(0.001f, 0);
    public bool UpdateFromShader = false; //set this to true if you modify the following shader's parameters at runtime without using the Set-functions in this script

    private float cloudsAmount;
    private float cloudsHorizonAmount;
    private float cloudThickness;
    private Vector4 offset;
    private float scale;
    private float shapeChangeSpeed;

    private MeshCollider meshCollider;
    private Material cloudsmat;
    private Texture2D cloudsTexture;

    public void SetClouds(float value)
    {
        cloudsAmount = value;
        cloudsmat.SetFloat("_Clouds", value);
    }
    public void SetCloudsHorizon(float value)
    {
        cloudsHorizonAmount = value;
        cloudsmat.SetFloat("_HorizonClouds", value);
    }
    public void SetThickness(float value)
    {
        cloudThickness = value;
        cloudsmat.SetFloat("_CloudThickness", value);
    }
    public void SetLightColor(Color value)
    {
        cloudsmat.SetColor("_cloudColorLight", value);
    }
    public void SetDarkColor(Color value)
    {
        cloudsmat.SetColor("_cloudColorDark", value);
    }
    public void SetFogColor(Color value)
    {
        cloudsmat.SetColor("_fogColor", value);
    }
    public void SetFogIntensity(float value)
    {
        cloudsmat.SetFloat("_fog", value);
    }
    public void SetFadeOut(float value)
    {
        cloudsmat.SetFloat("_FadeOut", value);
    }

    void Awake()
    {
        //sun = SceneInstanceManager.instance.sceneSun;
        cloudsmat = GetComponent<MeshRenderer>().material;
        cloudsTexture = cloudsmat.GetTexture("_MainTex") as Texture2D;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = false; //we don't want it to hinder anything

        cloudsAmount = cloudsmat.GetFloat("_Clouds");
        cloudsHorizonAmount = cloudsmat.GetFloat("_HorizonClouds");
        cloudThickness = cloudsmat.GetFloat("_CloudThickness");
        offset = cloudsmat.GetVector("_Offset");
        scale = cloudsmat.GetFloat("_Scale");
        shapeChangeSpeed = cloudsmat.GetFloat("_ShapeChangeSpeed");

        MaxSunIntensity = sun.intensity;
    }
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position;

        if (UpdateFromShader) //no need to update these as long as no other script modifies these values
        {
            offset = cloudsmat.GetVector("_Offset");
        }
        offset.x += WindSpeed.x * Time.deltaTime;
        offset.y += WindSpeed.y * Time.deltaTime;
        cloudsmat.SetVector("_Offset", offset);

        meshCollider.enabled = true;
        RaycastHit hit;        
        if (meshCollider.Raycast(new Ray(transform.position, -sun.transform.forward), out hit, 1000))
        {
            if (UpdateFromShader) //no need to update these as long as no other script modifies these values
            {
                cloudsAmount = cloudsmat.GetFloat("_Clouds"); 
                cloudsHorizonAmount = cloudsmat.GetFloat("_HorizonClouds");
                cloudThickness = cloudsmat.GetFloat("_CloudThickness");
                shapeChangeSpeed = cloudsmat.GetFloat("_ShapeChangeSpeed");
                scale = cloudsmat.GetFloat("_Scale");
            }
            if (CloudsDimSunlight)
            {
                sun.intensity = GetLightIntensity(hit.textureCoord) * MaxSunIntensity;
            }
            cloudsmat.SetVector("_SunDir", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
        }
        else
        {
            //Debug.LogWarning("couldn't sample sunposition");
        }
        meshCollider.enabled = false; //we don't want it to hinder anything
    }

    private float GetLightIntensity(Vector2 SunPos)
    {
        Vector2 distancexy = (SunPos - new Vector2(0.5f, 0.5f)) * 2;
        float distance = Mathf.Clamp01(distancexy.magnitude);
        float cloudfactor = cloudsAmount * (1 - distance) + cloudsHorizonAmount * distance;
        SunPos = SunPos + new Vector2(offset.x, offset.y);
        SunPos /= scale;
        //Debug.Log("cloudfactor1: " + cloudfactor);
        //var sample = cloudsTexture.GetPixelBilinear(SunPos.x, SunPos.y);
        var sample = cloudsTexture.GetPixelBilinear(SunPos.x - Time.timeSinceLevelLoad * shapeChangeSpeed * 0.05f, SunPos.y);
        sample.g = cloudsTexture.GetPixelBilinear(SunPos.x + Time.timeSinceLevelLoad * shapeChangeSpeed * 0.05f, SunPos.y).g;
        float factor = cloudsTexture.GetPixelBilinear(SunPos.x * 0.4f, SunPos.y * 0.4f + Time.timeSinceLevelLoad * shapeChangeSpeed * 0.0835f).b;//(Mathf.Sin(Time.time / 8.0f) + 1) * 0.5f;//(_SinTime.x + 1) * 0.5;
        float clouds = sample.r * factor + sample.g * (1 - factor); //shape change
        cloudfactor = Mathf.Clamp01((clouds - (1 - cloudfactor)) * cloudThickness);
        //cloudfactor *= 1 - Mathf.Pow(distance, 1 / fog);
        //Debug.Log("cloudfactor2: " + cloudfactor);
        return 1 - cloudfactor* cloudfactor * shadowFactor;
    }
}
