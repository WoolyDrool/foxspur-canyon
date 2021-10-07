using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Audio;
using Project.Runtime.Global;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneLoadingManager sceneLoader;
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

    [Header("Control Booleans")]
    public bool isPaused;
    private void Awake()
    {
        instance = this;
        
        if(!fade.activeSelf)
            fade.SetActive(true);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (sceneLoader == null)
        {
            sceneLoader = GameObject.FindObjectOfType<SceneLoadingManager>();
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitToMenu()
    {
        sceneLoader.LoadScene("scn_MainMenu");
    }
}
