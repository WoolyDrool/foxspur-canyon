using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenFarAway : MonoBehaviour
{
    public float enableDistance;
    public float disableDistance;

    public OcclusionPortal objectToEnable;
    
    private Transform playerTransform;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(playerTransform.position, transform.position);
        
        if (currentDistance > disableDistance)
        {
            objectToEnable.open = true;
            return;
        }

        if (currentDistance < enableDistance)
        {
            objectToEnable.open = false;
        }
    }
}
