using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager i;
    [Header("InputAsset")]
    private InputActionAsset asset;

    public PlayerInput _Input;

    private InputActionMap _inputActionMap;
    public InputDevice currentDevice;
    public bool gamepad = false;
    public Vector2 currentSensitivity;
    public Vector2 mouseXYSensitivity;
    public Vector2 gamepadXYSensitivity;

    [Header("Control Variables")]
    [SerializeField]
    private Vector2 inputMove;
    [SerializeField]
    private Vector2 inputLook;

    [Header("Bools")] 
    public bool inputWalk;
    public bool interact;
    public bool holdInteract;
    public bool grabbing;
    public bool fire1;
    public bool fire2;
    public bool crouch;
    public bool crouching;
    public bool inputSprint;
    public bool flashLightToggle;
    public bool flashLightHoldReload;
    public bool shoulderRight;
    public bool shoulderLeft;
   
    public bool journal;
    public bool map;
    public bool notes;
    public bool quests;
    

    #region Internal Variables
    
    //Movement
    internal InputAction moveAction;
    internal InputAction lookAction;
    internal InputAction jumpAction;
    internal InputAction sprintAction;
    internal InputAction crouchAction;
    
    //Interactions
    internal InputAction interactAction;
    internal InputAction hold_interactAction;
    internal InputAction grabbingInteractAction;
    
    //Tool
    internal InputAction fire1Action;
    internal InputAction fire2Action;
    internal InputAction reloadAction;
    internal InputAction nextToolAction;
    internal InputAction prevToolAction;
    internal InputAction flashlightAction;
    
    //Menus
    internal InputAction pauseAction;
    internal InputAction journalAction;
    internal InputAction mapAction;
    internal InputAction notesAction;
    internal InputAction questsAction;
    
    private Vector2 previous;
    private Vector2 _down;

    private int jumpTimer;
    private bool jump;

    private Gamepad curGamepad = Gamepad.current;
    private Keyboard curKeyboard = Keyboard.current;
    private Mouse curMouse = Mouse.current;

    #endregion
    
    private void Awake()
    {
        i = this;

        _Input = GetComponent<PlayerInput>();

        asset = _Input.actions;

        Init();
    }
    
    public void Init()
    {
        //Movement
        moveAction = asset["Move"];
        lookAction = asset["Look"];
        jumpAction = asset["Jump"];
        sprintAction = asset["Sprint"];
        crouchAction = asset["Crouch"];
        
        //Interactions
        interactAction = asset["Interact"];
        hold_interactAction = asset["Interact"];
        grabbingInteractAction = asset["Grab"];
        
        //Tools
        flashlightAction = asset["Flashlight"];
        fire1Action = asset["Fire1"];
        fire2Action = asset["Fire2"];
        prevToolAction = asset["PrevTool"];
        nextToolAction = asset["NextTool"];
        
        //Menus
        pauseAction = asset["Pause"];
        journalAction = asset["Journal"];
        mapAction = asset["Map"];
        notesAction = asset["Notes"];
        questsAction = asset["Quests"];
        
        asset.Enable();

        mouseXYSensitivity = new Vector2(GameManager.instance.settingsManager.playerPrefMouseSensitivity, GameManager.instance.settingsManager.playerPrefMouseSensitivity);
        gamepadXYSensitivity =
            mouseXYSensitivity;  /*new Vector2(GameManager.instance.settingsManager.playerPrefGamepadSensitivityX,
            GameManager.instance.settingsManager.playerPrefGamepadSensitivityY);*/

        /* =>
        {
            IsUsingGamepad();
            switch (change)
            {
                case InputDeviceChange.Added:
                    break;
                case InputDeviceChange.Disconnected: break;
                case InputDeviceChange.Reconnected:
                    break;
                case InputDeviceChange.Removed: break;
                default:
                    break;
            }
        };*/
    }
    
    void Start()
    {
        jumpTimer = -1;
        i = this;
    }

    void Update()
    {
        //InputSystem.Update();

        #region Movement

        inputMove = moveAction.ReadValue<Vector2>();
        inputLook = lookAction.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.K))
            gamepad = !gamepad;
        
        currentSensitivity = gamepad ? gamepadXYSensitivity : mouseXYSensitivity;

        inputWalk = GameManager.instance.playerManager._playerController.status == Status.walking;
        inputSprint = sprintAction.phase.IsInProgress();

        jump = jumpAction.phase.IsInProgress();
        //inputSprint = sprintAction.triggered;
        crouch = crouchAction.triggered;

        #endregion

        #region Interactions
        
        interact = interactAction.triggered;
        holdInteract = hold_interactAction.phase.IsInProgress();
        grabbing = grabbingInteractAction.triggered;
        
        #endregion

        #region Tools
        
        flashLightToggle = flashlightAction.triggered;
        flashLightHoldReload = flashlightAction.phase.IsInProgress();
                
        fire1 = fire1Action.triggered;
        fire2 = fire2Action.triggered;

        #endregion

        shoulderLeft = prevToolAction.triggered;
        shoulderRight = nextToolAction.triggered;

        #region Menus / UI
        
        journal = journalAction.triggered;
        map = mapAction.triggered;
        notes = notesAction.triggered;
        quests = notesAction.triggered;

        #endregion
        
        
        _down = Vector2.zero;
        if (raw.x != previous.x)
        {
            previous.x = raw.x;
            if (previous.x != 0)
                _down.x = previous.x;
        }

        if (raw.y != previous.y)
        {
            previous.y = raw.y;
            if (previous.y != 0)
                _down.y = previous.y;
        }
    }
    
    private void IsUsingGamepad(PlayerInput obj)
    {
        if (obj.currentControlScheme == "Gamepad")
        {
            gamepad = true;
        }
        else
        {
            gamepad = false;
        }
    }

    #region Initialization

    public void OnEnable()
    {
        _Input.controlsChangedEvent.AddListener(IsUsingGamepad); 
        
        pauseAction.performed += OnPause;

        moveAction.started += OnMove;
        sprintAction.performed += OnSprint;

        journalAction.performed += OnOpenJournalInventory;
        mapAction.performed += OnOpenJournalMap;
        
        
        //lookAction.actionMap.actionTriggered += OnLook;
        flashlightAction.performed += OnFlashlight;
    }

    private void OnDisable()
    {
        _Input.controlsChangedEvent.RemoveListener(IsUsingGamepad); 
        
        pauseAction.performed -= OnPause;

        moveAction.started -= OnMove;
        sprintAction.performed -= OnSprint;
        
        journalAction.performed -= OnOpenJournalInventory;
        mapAction.performed -= OnOpenJournalMap;
        
        
        //lookAction.actionMap.actionTriggered -= OnLook;
        flashlightAction.performed -= OnFlashlight;
    }

    #endregion

    #region Movement Bindings

    public void OnMove(InputAction.CallbackContext value)
    {

    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext value)
    {
        var ctx = value.ReadValue<float>();
        crouch = ctx > 0;
        crouching = crouch;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
    }
    
    public bool Jump()
    {
        return jump;
    }

    public void ResetJump()
    {
        jumpTimer = -1;
    }
    
    #endregion

    #region Interact Bindings

    public void OnInteract(InputAction.CallbackContext value)
    {
        interact = value.ReadValueAsButton();
    }
    
    public void OnHoldInteract(InputAction.CallbackContext value)
    {
        interact = interactAction.phase.IsInProgress();
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        var ctx = value.ReadValueAsButton();
        fire1 = ctx;
    }
    
    public void OnAltFire(InputAction.CallbackContext value)
    {
        var ctx = value.ReadValue<float>();
        //shooting = ctx > 0;
    }

    #endregion

    #region Tool Bindings

    public void OnFlashlight(InputAction.CallbackContext value)
    {
        var ctx = value.performed;
        if(!ctx)
            return;
        flashLightToggle = ctx;
    }
    
    #endregion

    #region Menu / UI Bindings

    public void OnPause(InputAction.CallbackContext value)
    {
        EventManager.TriggerEvent("PauseGame", null);
    }
    
    public void OnOpenJournalInventory(InputAction.CallbackContext value)
    {
        EventManager.TriggerEvent("OpenJournal", null);
        var ctx = value.ReadValueAsButton();
        journal = ctx;
    }

    public void OnOpenJournalMap(InputAction.CallbackContext value)
    {
        EventManager.TriggerEvent("OpenMap", null);
        var ctx = value.ReadValueAsButton();
        map = ctx;
    }

    public void OnOpenJournalNotes(InputAction.CallbackContext value)
    {
        EventManager.TriggerEvent("OpenNotes", null);
        var ctx = value.ReadValueAsButton();
        notes = ctx;
    }
    
    public void OnOpenJournalQuests(InputAction.CallbackContext value)
    {
        EventManager.TriggerEvent("OpenQuests", null);
        var ctx = value.ReadValueAsButton();
        quests = ctx;
    }

    #endregion
    
    #region Vectors

    public Vector2 input
    {
        get
        {
            Vector2 i = inputMove;
            //i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public Vector2 look
    {
        get
        {
            Vector2 il = lookAction.ReadValue<Vector2>();
            return il;
        }
    }

    public Vector2 down
    {
        get { return _down; }
    }

    public Vector2 raw
    {
        get
        {
            Vector2 i = inputMove;
            return i;
        }
    }

    #endregion

    #region Old Binding Code

    public float elevate
    {
        get
        {
            return Input.GetAxis("Elevate");
        }
    }

    /*public bool run
    {
        get { return ; }
    }

    public bool crouch
    {
        get { return Input.GetKeyDown(KeyCode.C); }
    }

    /*public bool crouching
    {
        get { return Input.GetKey(KeyCode.C); }
    }*/

    //public KeyCode interactKey
    //{ 
    //    get { return KeyCode.E; }
    //}

    /*public bool interact //(InputAction.CallbackContext value)
    {
        //return value.ReadValue<bool>();
        get { return Keyboard.current.eKey.isPressed; }
    }*/
    
    /*public bool hold_interact //(InputAction.CallbackContext value)
    {
        get { return Keyboard.current.eKey.isPressed; }
        //return value.ReadValue<bool>();
    }

    public bool reload
    {
        get { return Input.GetKeyDown(KeyCode.R); }
    }
    
    /*public bool flashLightToggle
    {
        get { return Input.GetKeyDown(KeyCode.F); }
    }*/

    

    public bool fixAction
    {
        get { return Input.GetKey(KeyCode.R); }
    }

    public bool aim
    {
        get { return Input.GetMouseButtonDown(1); }
    }

    public bool aiming
    {
        get { return Input.GetMouseButton(1); }
    }

    /*public bool shooting
    {
        get { return Input.GetMouseButtonDown(0); }
    }

    public bool grabbing
    {
        get { return Input.GetKeyDown(KeyCode.E); }
    }*/
    
    public bool resort
    {
        get { return Input.GetKeyDown(KeyCode.Y); }
    }

    public float mouseScroll
    { 
        get { return Input.GetAxisRaw("Mouse ScrollWheel"); }
    }

    #endregion
}
