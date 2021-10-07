using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugController : MonoBehaviour
{
    private bool _showConsole = false;

    private string _input;

    public static DebugCommand RESET_SCENE;

    public List<object> commandList;

    private void Awake()
    {
        RESET_SCENE = new DebugCommand("reset", "Reloads the current scene", "reset", () =>
        {
            //ProgressSceneLoader.loader.ReloadCurrentScene();
        });

        commandList = new List<object>
        {
            RESET_SCENE,
        };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (_showConsole)
            {
                _showConsole = false;
            }
            else
            {
                _showConsole = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_showConsole)
            {
                Debug.Log("Entered");
                HandleInput();
                _input = "";
            }
        }
    }

    private void OnGUI()
    {
        if(!_showConsole) { return; }

        float y = 0f;
        
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
    }

    private void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (_input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
