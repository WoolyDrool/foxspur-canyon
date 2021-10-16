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
    public bool isMusicCue;

    private AmbientSoundscape _ambientSoundscape;
    public AudioClip music;

    private BoxCollider bounds;
    private Vector3 playerPos;

    private void OnEnable()
    {
        bounds = GetComponent<BoxCollider>();
        _ambientSoundscape = FindObjectOfType<AmbientSoundscape>();
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
        if (isMusicCue)
        {
            if(!_ambientSoundscape.currentlyPlayingMusic)
                _ambientSoundscape.currentlyPlayingMusic = true;
            
            GameManager.instance.audioManager.PlayMusicTrack(music);
        }
        else
        {
            GameManager.instance.audioManager.SpawnAudioNearPlayer(playerPos, minDistance, maxDistance, soundCue);
        }
            
        gameObject.SetActive(false);
        
    }
}
