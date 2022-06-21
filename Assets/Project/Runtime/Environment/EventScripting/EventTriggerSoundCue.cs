using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Audio;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EventTriggerSoundCue : MonoBehaviour
{
    public bool triggerEnabled;

    public GameObject soundCue;
    public float minDistance;
    public float maxDistance;
    public bool isMusicCue;
    public bool alsoDisable = true;
    public bool todRequirement = false;
    public TimeManager.TimeOfDay tod;

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
            if (!todRequirement)
            {
                playerPos = other.transform.position;
                Trigger();
            }
            else
            {
                if (GameManager.instance.timeManager.time == tod)
                {
                    playerPos = other.transform.position;
                    Trigger();
                }
                else
                {
                    Debug.Log("Did not trigger event - TOD requirement not met");
                    return;
                }
            }
        }
    }

    private void Trigger()
    {
        EventTriggerSound();
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
            
        gameObject.SetActive(!alsoDisable);
        
    }
}
