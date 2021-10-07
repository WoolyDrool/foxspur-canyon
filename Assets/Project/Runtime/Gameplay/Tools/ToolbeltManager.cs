using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolbeltManager : MonoBehaviour
    {
        [Header("Equipped")]
        public EquippableObject currentlyEquipped;
        public EquippableObject[] equippableInventory;

        [Header("Tracking")]
        public Transform cameraMovement;
        public Transform container;
        
        [Header("UI")]
        public Color normalColor;
        public Color selectedColor;

        // TODO: Move this over to a dedicated UI manager
        public Image[] toolbarIcons;
        public TextMeshProUGUI selectedText;

        #region Internal Variables

        [SerializeField]private Quaternion _containerRot;
        [SerializeField] private Quaternion _trackerRot;
        [SerializeField] private Vector3 _containerStartPos;
        [SerializeField] private Vector3 _containerPosCrouch;
        private int _equippableIndex;
        private int _selectedTool = 0;
        private PlayerController _controller;
        private bool isCalled;

        #endregion
        

        private void Awake()
        {
            _controller = GetComponentInParent<PlayerController>();
        }

        private void Start()
        {
            SwitchEquipped();
        }

        private void Update()
        {
            int previousSelectedTool = _selectedTool;
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (_selectedTool >= equippableInventory.Length - 1)
                    _selectedTool = 0;
                else
                    _selectedTool++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (_selectedTool <= 0)
                    _selectedTool = equippableInventory.Length - 1;
                else
                    _selectedTool--;
            }

            if (previousSelectedTool != _selectedTool)
            {
                SwitchEquipped();
            }

            
            
            HandleLerp();
        }

        void ChangeCrouchPosition()
        {
            if (!isCalled)
            {
                container.localPosition = _containerPosCrouch;
                isCalled = true;
            }
            else
            {
                isCalled = false;
                container.localPosition = _containerStartPos;
            }
        }

        void HandleLerp()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeCrouchPosition();
            }
            
            _containerRot = container.localRotation;
            _trackerRot = cameraMovement.localRotation;
            container.localRotation = _trackerRot;
        }

        void SwitchEquipped()
        {
            if(!selectedText.gameObject.activeSelf)
                selectedText.gameObject.SetActive(true);
            _equippableIndex = 0;
            foreach (EquippableObject tool in equippableInventory)
            {
                if (_equippableIndex == _selectedTool)
                {
                    selectedText.gameObject.SetActive(true);
                    selectedText.text = tool.toolName;
                    toolbarIcons[_equippableIndex].color = selectedColor;
                    tool.ToggleEquip(true);
                }
                else
                {
                    toolbarIcons[_equippableIndex].color = normalColor;
                    tool.ToggleEquip(false);
                }

                _equippableIndex++;
            }
            
        }
    }
}