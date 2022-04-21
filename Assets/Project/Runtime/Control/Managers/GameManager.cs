using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Audio;
using Project.Runtime.Global;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneLoadingManager sceneLoader;
    public GameObject sceneLoaderPrefab;
    public static GameManager instance;
    public GameObject fade;

    [Header("World Managers")]
    public WorldLightingManager lightingManager;
    public GlobalAudioMixer audioManager;
    public TimeManager timeManager;
    public ControlsManager controlsManager;
    
    [Header("Player Managers")]
    public PlayerManager playerManager;
    public PlayerInventory playerInventory;
    public PlayerVitals playerVitals;
    public CameraMovement cameraManager;

    [Header("Player Information")] 
    public Vector3 currentPlayerPosition;

    [Header("Control Booleans")]
    public bool isPaused;
    private void Awake()
    {
        instance = this;
        
        if(!fade.activeSelf)
            fade.SetActive(true);

        if (sceneLoader == null)
        {
            
        }
        else
        {
            return;
        }
    }

    IEnumerator SanityCheck()
    {
        if (sceneLoader == null)
        {
            sceneLoader = GameObject.FindObjectOfType<SceneLoadingManager>();
            yield return sceneLoader;
        }
    }

    private void Update()
    {
        currentPlayerPosition = playerManager.playerTransform.position;
    }

    public void QuitToMenu()
    {
        sceneLoader.LoadScene("scn_MainMenu");
    }
}
