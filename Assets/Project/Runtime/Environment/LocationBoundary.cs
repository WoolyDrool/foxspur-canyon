using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LocationBoundary : MonoBehaviour
{
    public PlayerManager playerManager;
    public string locationName;
    private AudioSource source;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerManager.ChangeLocation(locationName);
            source.Play();
            
        }
    }
}
