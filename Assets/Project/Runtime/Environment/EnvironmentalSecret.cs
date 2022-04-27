using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.UI.Elements;
using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentalSecret : MonoBehaviour
{
    public AudioClip discoverSound;
    public AudioMixerGroup MixerGroup;
    private PlayerInventory _inventory;
    public bool isTrailSecret = true;
    void Start()
    {
        _inventory = GameManager.instance.playerInventory;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.audioManager.PlaySoundOnce(discoverSound, MixerGroup);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.SECRET, "You found a secret!");
            if (isTrailSecret)
            {
                RuntimeTrailManager tm = FindObjectOfType<RuntimeTrailManager>();
                tm.secretsFound++;
            }
            else
            {
                _inventory.currentSecrets++;
            }
            Destroy(gameObject);
        }
    }
}
