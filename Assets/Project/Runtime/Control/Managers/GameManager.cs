using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime;
using Project.Runtime.Audio;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Global;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.Serialization;
using Project.Runtime.UI.HUD;
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
    public PlayerInputManager inputManager;
    public SettingsManager settingsManager;
    
    [Header("Save Managers")]
    public PersistentPlayerCharacter ppc;
    public RuntimeProfileInterpreter rpi;
    public PlayerProfileInterpreter ppi;
    
    [Header("UI Managers")] 
    public UIElementsHudManager hudManager;
    public HudInteractableController hudInteractionController;
    public HudInteraction hudController;

    [Header("Player Information")] 
    public Vector3 currentPlayerPosition;

    public Quaternion currentPlayerRotation;

    [Header("Control Booleans")] 
    public bool toggleSprint = false;
    [SerializeField]
    public bool _isPaused = false;
    [SerializeField]
    bool _showMouse = false;
    [SerializeField]
    bool _freeLook = true;
        
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

    private void OnEnable()
    {
        // Global Commands
        EventManager.StartListening("PauseGame", PauseGame);
        EventManager.StartListening("FreeMouse", FreeMouse);
        EventManager.StartListening("HideMouse", HideMouse);
        EventManager.StartListening("FreeLook", FreeLook);
        EventManager.StartListening("ReturnToNormal", ReturnToNormal);
        
        // UI Commands
        EventManager.StartListening("OpenJournal", OpenJournal);
        EventManager.StartListening("OpenMap", OpenMap);
        EventManager.StartListening("OpenQuests", OpenQuests);
        EventManager.StartListening("OpenNotes", OpenNotes);
        
        EventManager.StartListening("ClearUI", ClearUI);
    }
    
    private void OnDisable()
    {
        // Global Commands
        EventManager.StopListening("PauseGame", PauseGame);
        EventManager.StopListening("FreeMouse", FreeMouse);
        EventManager.StartListening("HideMouse", HideMouse);
        EventManager.StopListening("FreeLook", FreeLook);
        EventManager.StopListening("ReturnToNormal", ReturnToNormal);
        
        // UI Commands
        EventManager.StopListening("OpenJournal", OpenJournal);
        EventManager.StopListening("OpenMap", OpenMap);
        EventManager.StopListening("OpenQuests", OpenQuests);
        EventManager.StopListening("OpenNotes", OpenNotes);
        
        EventManager.StopListening("ClearUI", ClearUI);
    }
    
    
    void Init()
    {
        if (!sceneLoader)
        {
            try
            {
                sceneLoader = GetComponentInChildren<SceneLoadingManager>();
            }
            catch (Exception e)
            {
                Debug.LogWarning("No SceneLoadingManager found! Console: " + e);
                throw;
            }
        }

        if (!ppc)
        {
            try
            {
                ppc = GetComponentInParent<PersistentPlayerCharacter>();
            }
            catch (Exception e)
            {
                Debug.LogWarning("No PersistentPlayerCharacter found! Console: " + e);
                Console.WriteLine(e);
                throw;
            }
        }
        
        if(!sceneLoader || !ppc)
            Application.Quit(); Console.WriteLine("Critical Issue: No SceneLoadingManager or PeristentPlayerCharacter found. Closing...");

        settingsManager.GetPlayerSettingPreferences();
    }
    
    #region Global Game Commands

    public void PauseGame(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.PAUSE);
        _isPaused = true;
        _showMouse = true;
        _freeLook = false;
        Time.timeScale = 0;
    }
    
    public void FreeMouse(Dictionary<string, object> message)
    {
        _showMouse = true;
    }
    
    public void HideMouse(Dictionary<string, object> message)
    {
        _showMouse = false;
    }
    
    public void FreeLook(Dictionary<string, object> message)
    {
        _freeLook = !_freeLook;
    }
    
    public void ReturnToNormal(Dictionary<string, object> message)
    {
        Time.timeScale = 1;
        _isPaused = false;
        _showMouse = false;
        _freeLook = true;
    }

    #endregion

    #region UI Commands

    public void OpenJournal(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.INV);
    }
    
    public void OpenMap(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.MAP);
    }
    
    public void OpenQuests(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.QUESTS);
    }
    
    public void OpenNotes(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.NOTES);
    }

    public void ClearUI(Dictionary<string, object> message)
    {
        hudManager.ChangePlayerHudState(PlayerHudState.NONE);
    }

    #endregion

    private void Update()
    {
        currentPlayerPosition = playerManager.playerTransform.position;
        currentPlayerRotation = playerManager.playerTransform.rotation;

        Cursor.visible = _showMouse;
        Cursor.lockState = _showMouse ? CursorLockMode.None : CursorLockMode.Locked;
        cameraManager.canLook = _freeLook;
    }
}
