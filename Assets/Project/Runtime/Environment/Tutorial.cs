using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Global;
using UnityEngine;

namespace Project.Runtime
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject currentPanel;

        public HudInteraction hudController;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (!hudController)
                hudController = GameManager.instance.hudController;
                
            if (Input.GetKeyDown(KeyCode.F8))
            {
                OpenBugReportPage();    
            }
        }

        void OpenBugReportPage()
        {
            Application.OpenURL("https://forms.gle/yoSJe5c43cnfDM137");
        }

        public void SwitchTutorialPanel(GameObject nextPanel)
        {
            currentPanel = null;

            currentPanel = nextPanel;
            if(!currentPanel.activeSelf)
                currentPanel.SetActive(true);
            
            EventManager.TriggerEvent("FreeMouse", null);
            EventManager.TriggerEvent("FreeLook", null);
        }

        public void CloseTutorialPanel()
        {
            currentPanel.SetActive(false);
            EventManager.TriggerEvent("FreeMouse", null);
            EventManager.TriggerEvent("FreeLook", null);
        }
    }
}
