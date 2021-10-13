using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;
	
    // How long the object should shake for.
    public float shakeDuration = 0f;
	
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public bool shouldShake = false;
	
    Vector3 originalPos;
	
    void Awake()
    {
        EventManager.StartListening("EnableShake", EnableShake);
        EventManager.StartListening("DisableShake", DisableShake);
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
	
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void EnableShake(Dictionary<string, object> message)
    {
        shouldShake = true;
    }

    void DisableShake(Dictionary<string, object> message)
    {
        shouldShake = false;
    }

    void Update()
    {
        if (shouldShake)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
            //shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            //shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
