using System;
using Project.Runtime.Global;
using Project.Runtime.Serialization;
using Project.Runtime.UI.Menus;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonSelectSave : MonoBehaviour
    {
        private string saveName;
        public TextMeshProUGUI saveText;
        public GameObject deleteSaveButton;
        public GameObject deleteSavePopup;
        private RuntimeProfileInterpreter runtimeProfile;
        private PlayerProfile loadedProfile;
        private UIButtonChangePanel _panel;
        private UIMenuMain _main;
        void Start()
        {
            runtimeProfile = FindObjectOfType<RuntimeProfileInterpreter>();
            _main = FindObjectOfType<UIMenuMain>();
            _panel = GetComponent<UIButtonChangePanel>();
        }

        void OnEnable()
        {
            
            
            if (SerializationManager.TryGetProfile())
            {
                loadedProfile = SerializationManager.Load();
                runtimeProfile.InjectProfile(loadedProfile);
                saveName = loadedProfile.playerName;
                saveText.text = saveName;
                deleteSavePopup.SetActive(true);
            }
            else
            {
                loadedProfile = null;
                Debug.LogAssertion("No valid save found");
                saveText.text = "Empty";
                deleteSaveButton.SetActive(false);
                return;
            }
        }

        public void DetermineNext()
        {
            if (loadedProfile == SerializationManager.Load())
            {
                runtimeProfile.InjectProfile(loadedProfile);
                _main.sceneToLoad = "MainGameWorld";
                _main.OnClick();
                return;
            }
            else
            {
                _panel.OnClick();
            }
            
        }

        public void CheckIfSure()
        {
            deleteSavePopup.SetActive(true);
        }

        public void DontDelete()
        {
            deleteSavePopup.SetActive(false);
        }

        public void DeleteSave()
        {
            deleteSavePopup.SetActive(false);
            saveText.text = "Empty";
            deleteSaveButton.SetActive(false);
            SerializationManager.Delete(loadedProfile);
            loadedProfile = null;
            RefreshEditorProjectWindow();
        }
        
        void RefreshEditorProjectWindow() 
        {
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif
        }
    }

}