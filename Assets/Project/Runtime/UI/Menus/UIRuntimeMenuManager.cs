using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.UI.Elements;
using UnityEngine;
using Project.Runtime.Global;

namespace Project.Runtime.UI.Menus
{
    public enum MenuState
    {
        DEFAULT,
        NONE,
        PAUSE,
        INVENTORY,
        DEATH,
        PAYOUT
    }
    
    public class UIRuntimeMenuManager : MonoBehaviour
    {
        public MenuState currentMenuState;
        public MenuState defaultMenuState;
        public MenuState previousMenuState;
        
        [Header("Menus")] 
        public UIInventoryView inventory;
        public UIPauseMenu pauseMenu;
        public GameObject deathMenu;
        public GameObject trailPayoutMenu;

        void Start()
        {
            DefineMenuIndex();
            EventManager.StartListening("ShowDeathMenu", DeathMenu);
        }

        private void DefineMenuIndex()
        {
            defaultMenuState = MenuState.NONE;
            currentMenuState = defaultMenuState;
        }

        public void UpdateMenuIndex(MenuState state)
        {
            previousMenuState = currentMenuState;
            currentMenuState = state;
            HandleMenuSwitching();
        }

        void DeathMenu(Dictionary<string, object> message)
        {
            currentMenuState = MenuState.DEATH;
        }

        void Update()
        {
            if (currentMenuState == MenuState.NONE || currentMenuState != MenuState.DEATH)
            {
                if (Input.GetKeyDown(KeyCode.I) && currentMenuState != MenuState.PAUSE)
                {
                    UpdateMenuIndex(MenuState.INVENTORY);
                }

                if (Input.GetKeyDown(KeyCode.Escape) && currentMenuState != MenuState.INVENTORY)
                {
                    UpdateMenuIndex(MenuState.PAUSE);
                }
            }


            if (currentMenuState == MenuState.INVENTORY)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UpdateMenuIndex(MenuState.NONE);
                }
                
                if (Input.GetKeyDown(KeyCode.I) && inventory.gameObject.activeSelf)
                {
                    UpdateMenuIndex(MenuState.NONE);
                }
            }

            if (currentMenuState == MenuState.PAUSE)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.gameObject.activeSelf)
                {
                    UpdateMenuIndex(MenuState.NONE);
                }
            }

            if (currentMenuState == MenuState.PAYOUT)
            {
                
            }

            pauseMenu.gameObject.SetActive(currentMenuState == MenuState.PAUSE);
            inventory.gameObject.SetActive(currentMenuState == MenuState.INVENTORY);
            deathMenu.SetActive(currentMenuState == MenuState.DEATH);
            trailPayoutMenu.SetActive(currentMenuState == MenuState.PAYOUT);
        }

        public void Resume()
        {
            UpdateMenuIndex(MenuState.NONE);
        }

        
        // Define gamestate functions when in X menu
        void HandleMenuSwitching()
        {
            switch (currentMenuState)
            {
                case MenuState.NONE:
                {
                    EventManager.TriggerEvent("ReturnToNormal", null);
                    break;
                }
                case MenuState.INVENTORY:
                {
                    EventManager.TriggerEvent("ToggleMouse", null);
                    EventManager.TriggerEvent("FreeLook", null);
                    EventManager.TriggerEvent("InventoryMenu", null);
                    break;
                }
                case MenuState.PAUSE:
                {
                    EventManager.TriggerEvent("ToggleMouse", null);
                    EventManager.TriggerEvent("FreeLook", null);
                    EventManager.TriggerEvent("PauseMenu", null);
                    break;
                }
                case MenuState.DEATH:
                {
                    EventManager.TriggerEvent("ToggleMouse", null);
                    break;
                }
                case MenuState.PAYOUT:
                {
                    EventManager.TriggerEvent("ToggleMouse", null);
                    EventManager.TriggerEvent("PayoutMenu", null);
                    break;
                }

            }
        }
    }

}