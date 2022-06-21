using System;
using System.Collections.Generic;
using Project.Runtime.Global;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Menus
{
    public class UIGenericMenuObject : MonoBehaviour
    {
        public bool isPriority;
        public RectTransform[] graphicalElements; 
        public TextMeshProUGUI[] textElements;

        private bool _canvas;
        private bool _text;

        private void Awake()
        {
            #region Toggles
            EventManager.StartListening("UI_ToggleJournal", ToggleJournal);
            EventManager.StartListening("UI_ToggleInventory", ToggleInventory);
            EventManager.StartListening("UI_ToggleMap", ToggleMap);
            EventManager.StartListening("UI_ToggleQuests", ToggleQuests);
            #endregion

            #region States

            EventManager.StartListening("UI_Pause", ToggleQuests);
            EventManager.StartListening("UI_GameOver", ToggleQuests);
            EventManager.StartListening("UI_QuestComplete", ToggleQuests);
            EventManager.StartListening("UI_TrailComplete", ToggleQuests);

            #endregion
        }

        private void ToggleQuests(Dictionary<string, object> obj)
        {
            throw new NotImplementedException();
        }

        private void ToggleMap(Dictionary<string, object> obj)
        {
            throw new NotImplementedException();
        }

        private void ToggleInventory(Dictionary<string, object> obj)
        {
            throw new NotImplementedException();
        }

        private void ToggleJournal(Dictionary<string, object> obj)
        {
            throw new NotImplementedException();
        }

        public virtual void ActivateMenu()
        {
            for (int i = 0; i < graphicalElements.Length; i++)
            {
                graphicalElements[i].gameObject.SetActive(true);
            }
            
            for (int i = 0; i < textElements.Length; i++)
            {
                textElements[i].gameObject.SetActive(true);
            }
        }

        public void ToggleElements()
        {
            _canvas = !_canvas;
            for (int i = 0; i < graphicalElements.Length; i++)
            {
                graphicalElements[i].gameObject.SetActive(_canvas);
            }
        }

        public void ToggleText()
        {
            _text = !_text;
            
            for (int i = 0; i < graphicalElements.Length; i++)
            {
                graphicalElements[i].gameObject.SetActive(_text);
            }
        }

    }
}