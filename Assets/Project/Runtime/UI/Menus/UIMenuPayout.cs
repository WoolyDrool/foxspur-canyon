using System;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Global;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Menus
{
    public class UIMenuPayout : MonoBehaviour
    {
        public TextMeshProUGUI trailNameTxt;
        public TextMeshProUGUI basePayoutTxt;
        public TextMeshProUGUI secretsTxt;
        public TextMeshProUGUI moneyRewardTxt;
        public TextMeshProUGUI progressTxt;
        public RuntimeTrailManager trailManager;
        public RectTransform mainPanel;

        private int _moneyPayout;

        private void Awake()
        {
            trailManager = FindObjectOfType<RuntimeTrailManager>();
            EventManager.StartListening("FinishTrail", BeginPayoutSequence);
            //mainPanel.gameObject.SetActive(false);
        }

        public void LateUpdate()
        {
            if(trailManager)
                progressTxt.text = trailManager.completionPercentageN + "/100";
        }

        private void BeginPayoutSequence(Dictionary<string, object> message)
        {
            mainPanel.gameObject.SetActive(true);
            trailNameTxt.text = trailManager.trailData.TrailName;
            _moneyPayout = trailManager.totalPayout;
            moneyRewardTxt.text = _moneyPayout.ToString();
            basePayoutTxt.text = trailManager.trailCompletionPayout.ToString();
            secretsTxt.text = trailManager.secretsPayout.ToString();
            EventManager.TriggerEvent("ToggleMouse", null);
            EventManager.TriggerEvent("FreeLook", null);
        }

        public void ExitButton()
        {
            EventManager.TriggerEvent("ReturnToNormal", null);
            mainPanel.gameObject.SetActive(false);
            SceneLoadingManager.loader.LoadSceneAdditive(trailManager.destinationScene);
        }
    }
    
    
}