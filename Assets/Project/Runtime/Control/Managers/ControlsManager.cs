using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public bool _isPaused;
    public bool _showInventory;
    private bool _showMouse = false;
    private bool _freeLook = true;

    private void OnEnable()
    {
        Time.timeScale = 1;
        EventManager.StartListening("ToggleMouse", ToggleMouse);
        EventManager.StartListening("PauseMenu", Pause);
        EventManager.StartListening("InventoryMenu", Inventory);
        EventManager.StartListening("FreeLook", ToggleFreeLook);
        EventManager.StartListening("ReturnToNormal", ReturnToNormal);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ToggleMouse", ToggleMouse);
        EventManager.StopListening("PauseMenu", Pause);
        EventManager.StopListening("InventoryMenu", Inventory);
        EventManager.StopListening("FreeLook", ToggleFreeLook);
        EventManager.StopListening("ReturnToNormal", ReturnToNormal);
    }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _showMouse = false;
    }

    void Update()
    {
        //TODO: Migrate player controls over into this class

        _isPaused = GameManager.instance.isPaused;
    }

    public void ReturnToNormal(Dictionary<string, object> message)
    {
        GameManager.instance.cameraManager.canLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

    public void ToggleMouse(Dictionary<string, object> message)
    {
        if (!_showMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _showMouse = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _showMouse = false;
        }
        
    }

    public void ToggleFreeLook(Dictionary<string, object> message)
    {
        if (_freeLook)
        {
            _freeLook = false;
        }
        else
        {
            _freeLook = true;
        }
        GameManager.instance.cameraManager.canLook = _freeLook;
    }
    
    public void Pause(Dictionary<string, object> message)
    {
        if (!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            _isPaused = false;
            Time.timeScale = 1;
        }
        
    }
    

    public void Inventory(Dictionary<string, object> message)
    {
        if (!_showInventory)
        {
            _showInventory = true;
        }
        else
        {
            _showInventory = false;
        }
    }
}
