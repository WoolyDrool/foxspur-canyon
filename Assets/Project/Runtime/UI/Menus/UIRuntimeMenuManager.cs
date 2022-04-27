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

    public enum TabState
    {
        QUESTS,
        INVENTORY,
        MAP,
        NOTES
    }
    
    public class UIRuntimeMenuManager : MonoBehaviour
    {
        public MenuState currentMenuState;
        public MenuState defaultMenuState;
        public MenuState previousMenuState;

        public TabState currentTab;
        public TabState defaultTab;
        public TabState previousTab;

        [Header("Menus")] public GameObject journalPanel;
        public UIInventoryView inventory;
        public UIPauseMenu pauseMenu;
        public GameObject deathMenu;
        public GameObject questMenu;
        public GameObject inventoryMenu;
        public GameObject mapMenu;
        public GameObject notesMenu;
        public GameObject trailPayoutMenu;
        public List<GameObject> menus = new List<GameObject>();

        void Start()
        {
            DefineMenuIndex();
            EventManager.StartListening("ShowDeathMenu", DeathMenu);
            EventManager.StartListening("ShowJournal", ShowJournal);
        }

        private void ShowJournal(Dictionary<string, object> obj)
        {
            journalPanel.SetActive(!journalPanel.activeSelf);
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

        public void ChangeTab (int index)
        {
            switch (index)
            {
                case 0:
                {
                    currentTab = TabState.QUESTS;
                    break;
                }
                case 1:
                {
                    currentTab = TabState.INVENTORY;
                    break;
                }
                case 2:
                {
                    currentTab = TabState.MAP;
                    break;
                }
                case 3:
                {
                    currentTab = TabState.NOTES;
                    break;
                }
            }
        }

        void Update()
        {
            questMenu.SetActive(currentTab == TabState.QUESTS);
            inventoryMenu.SetActive(currentTab == TabState.INVENTORY);
            mapMenu.SetActive(currentTab == TabState.MAP);
            notesMenu.SetActive(currentTab == TabState.NOTES);
            
            /*if (currentMenuState == MenuState.NONE || currentMenuState != MenuState.DEATH)
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
           // trailPayoutMenu.SetActive(currentMenuState == MenuState.PAYOUT);*/
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