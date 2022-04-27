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
    private static GameManager _gameManager;
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

    public static GameManager instance
    {
        get
        {
            if (!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!_gameManager)
                {
                    Debug.LogError("No GameManager!");
                }
                else
                {
                    _gameManager.Init();
                        
                    //DontDestroyOnLoad(_eventManager);
                }
            }

            return _gameManager;
        }
    }

    void Init()
    {
        if(!fade.activeSelf)
            fade.SetActive(true);

        if (!sceneLoader)
        {
            sceneLoader = FindObjectOfType(typeof(SceneLoadingManager)) as SceneLoadingManager;
            if(!sceneLoader)
                Debug.LogError("No SceneLoadingManager!");
        }
    }

    private void Awake()
    {

        
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
