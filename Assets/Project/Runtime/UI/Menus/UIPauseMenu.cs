using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using Project.Runtime.UI.HUD;
using UnityEngine;

namespace Project.Runtime.UI.Menus
{
    public class UIPauseMenu : MonoBehaviour
    {
        [SerializeField]
        private UIElementsHudManager _hudManager;

        public GameObject optionsMenuObj;
        public GameObject tutorialMenuObj;
        public GameObject saveManagerMenuObj;
        void Start()
        {
            if (!_hudManager)
                _hudManager = GetComponentInParent<UIElementsHudManager>();
        }

        void Update()
        {
        
        }

        public void Resume()
        {
            _hudManager.ChangePlayerHudState(PlayerHudState.NONE);
            EventManager.TriggerEvent("ReturnToNormal", null);
        }
    }

}
