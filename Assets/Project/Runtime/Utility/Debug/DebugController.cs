using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugController : MonoBehaviour
{
    private bool _showConsole = false;

    private string _input;

    public static DebugCommand RESET_SCENE;
    public static DebugCommand FORCE_DEHYDRATION;
    public static DebugCommand FORCE_EXHAUSTION;
    public static DebugCommand FORCE_STARVATION;
    public static DebugCommand FORCE_DEATH;
    public static DebugCommand REMOVE_ALL_STATS;
    public static DebugCommand FORCE_SLEEBY;

    public List<object> commandList;

    private void Awake()
    {
        RESET_SCENE = new DebugCommand("reset", "Reloads the current scene", "reset", () =>
        {
            //ProgressSceneLoader.loader.ReloadCurrentScene();
        });
        
        FORCE_DEHYDRATION = new DebugCommand("force_dehydration", "Forces dehydration", "force_dehydration", () =>
        {
            VitalHydrationFunctionality hf = FindObjectOfType<VitalHydrationFunctionality>();
            hf.ForceDehydration();
        });
        
        FORCE_EXHAUSTION = new DebugCommand("force_exhaustion", "Forces exhaustion", "force_exhaustion", () =>
        {
            VitalSleepFunctionality sf = FindObjectOfType<VitalSleepFunctionality>();
            sf.ForceExhaustion();
        });
        
        FORCE_STARVATION = new DebugCommand("force_starvation", "Forces starvation", "force_starvation", () =>
        {
            VitalHungerFunctionality hung = FindObjectOfType<VitalHungerFunctionality>();
            hung.ForceStarvation();
        });
        
        FORCE_DEATH = new DebugCommand("kill", "Kills the player", "kill", () =>
        {
            VitalHealthFunctionality hf = FindObjectOfType<VitalHealthFunctionality>();
            hf.ForceDeath();
        });
        
        FORCE_SLEEBY = new DebugCommand("force_sleeby", "Kills the player", "force_sleeby", () =>
        {
            VitalSleepFunctionality sf = FindObjectOfType<VitalSleepFunctionality>();
            sf.ForceBlinking();
        });
        
        REMOVE_ALL_STATS = new DebugCommand("remove_all_stats", "Drains all players stats except health", "remove_all_stats", () =>
        {
            VitalHydrationFunctionality hf = FindObjectOfType<VitalHydrationFunctionality>();
            hf.ForceDehydration();
            VitalSleepFunctionality sf = FindObjectOfType<VitalSleepFunctionality>();
            sf.ForceExhaustion();
            VitalHungerFunctionality hung = FindObjectOfType<VitalHungerFunctionality>();
            hung.ForceStarvation();
        });

        commandList = new List<object>
        {
            RESET_SCENE,
            FORCE_DEHYDRATION,
            FORCE_EXHAUSTION,
            FORCE_STARVATION,
            FORCE_DEATH,
            REMOVE_ALL_STATS,
            FORCE_SLEEBY
        };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
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
        if (!_showConsole)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
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
