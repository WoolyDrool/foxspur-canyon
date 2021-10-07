using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Project.Runtime.Global;

public class AudioOcclusionZone : MonoBehaviour
{
    public AudioMixer groupToOcclude;
    public float occlusionAmount;
    public float defaultMixerValue;
    public bool crossfading;
    private float t;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //groupToOcclude.SetFloat("sfxLowpassFilterAmount", occlusionAmount);
            StartCoroutine(CrossFade("sfxLowpassFilterAmount", defaultMixerValue, occlusionAmount, 1f));
            //crossfading = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //groupToOcclude.SetFloat("sfxLowpassFilterAmount", defaultMixerValue);
            StartCoroutine(CrossFade("sfxLowpassFilterAmount", occlusionAmount, defaultMixerValue, 1f));
            //crossfading = true;
        }
    }
    
    IEnumerator CrossFade(string one, float value1, float value2,float crossfadeTime)
    {
        crossfading = true;
        
        while (crossfading)
        {
            Debug.Log("Crossfading");
            t += crossfadeTime * Time.deltaTime;
            float crossfade1 = Mathf.Lerp(value1, value2, t);

            groupToOcclude.SetFloat(one, crossfade1);
            yield return null;
            
            if (crossfade1 == value2)
            {
                crossfading = false;
                t = 0;
                crossfade1 = 0;
            }
        }

        crossfading = false;
        yield return null;
    }
}
