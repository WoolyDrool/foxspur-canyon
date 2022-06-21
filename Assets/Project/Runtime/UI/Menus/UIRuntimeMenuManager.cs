using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.UI.Elements;
using UnityEngine;
using Project.Runtime.Global;

namespace Project.Runtime.UI.Menus
{
    public class UIRuntimeMenuManager : MonoBehaviour
    {
        public UIGenericMenuObject nextMenu;
        public UIGenericMenuObject currentMenu;
        public UIGenericMenuObject previousMenu;

        // Make a new type of observable object and apply it to every UI panel element in the game
        // This class will not store a list of objects, but rather current, previous, and queued elements
        // Each element will have a priority field
        // most items will not be priority, but things like the pause menu/death menu/trail payouts will be
        // This class will handle the logic of switching and displaying each menu panel. It will check if an incoming panel request is priority or not and 

        public void SubmitMenuRequest(UIGenericMenuObject menuObject)
        {
            nextMenu = menuObject;
            if (currentMenu.isPriority)
            {
                if (nextMenu.isPriority)
                {
                    // Do Exception Code
                }
                else
                {
                    Debug.LogWarning("UI: Cannot display " + menuObject + ", " + currentMenu + " is marked as priority");
                    nextMenu = null;
                }
            }
            else
            {
                Debug.Log("UI: Switched from " + currentMenu + " to " + nextMenu);
                currentMenu = nextMenu;
                nextMenu = null;
            }
        }
    }
}