using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Audio;
using UnityEngine;


public enum EventType
{
    PLAYSOUND,
    SPAWNOBJECT,
    INCREASEAGGRESSION,
    
}
[RequireComponent(typeof(BoxCollider))]
public class EventTrigger : MonoBehaviour
{
    public EventType type;
    public bool triggerEnabled;

    public GameObject soundCue;
    public float minDistance;
    public float maxDistance;

    private BoxCollider bounds;
    private Vector3 playerPos;

    private void OnEnable()
    {
        bounds = GetComponent<BoxCollider>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPos = other.transform.position;
            Trigger();
        }
    }

    private void Trigger()
    {
        switch (type)
        {
            case EventType.PLAYSOUND:
            {
                EventTriggerSound();
                break;
            }
        }
    }

    void EventTriggerSound()
    {
        GameManager.instance.audioManager.SpawnAudioNearPlayer(playerPos, minDistance, maxDistance, soundCue);
        gameObject.SetActive(false);
    }
}
