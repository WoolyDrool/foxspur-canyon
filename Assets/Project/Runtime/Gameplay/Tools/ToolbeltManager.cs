using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private Quaternion _containerRot;
        private Quaternion _trackerRot;
        [SerializeField] private Vector3 _containerStartPos;
        [SerializeField] private Vector3 _containerPosCrouch;
        
        private int _equipIndex;
        private int _selectedTool = 0;
        private PlayerController _controller;
        private PlayerInputManager _inputManager;
        private InputAction _prev;
        private InputAction _next;
        

        #endregion
        

        private void Awake()
        {
            _controller = GetComponentInParent<PlayerController>();
            _inputManager = GetComponentInParent<PlayerInputManager>();
            _containerStartPos = container.localPosition;


            _prev = _inputManager.prevToolAction;
            _next = _inputManager.nextToolAction;
        }

        private void Start()
        {
            SwitchEquipped();
        }

        private void Update()
        {
            int previousSelectedTool = _selectedTool;
            
            if (_prev.triggered)
            {
                if (_selectedTool >= equippableInventory.Length - 1)
                    _selectedTool = 0;
                else
                    _selectedTool++;
            }
            if (_next.triggered)
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

        

        void HandleLerp()
        {
            ChangeCrouchPosition();

            _containerRot = container.localRotation;
            _trackerRot = cameraMovement.localRotation;
            container.localRotation = _trackerRot;
        }
        
        void ChangeCrouchPosition()
        {
            if (_controller.status == Status.crouching)
            {
                container.localPosition = _containerPosCrouch;
                //isCalled = true;
            }
            else
            {
                //isCalled = false;
                container.localPosition = _containerStartPos;
            }
        }

        public void IncreaseEquipIndex(InputAction.CallbackContext value)
        {
            if (_selectedTool >= equippableInventory.Length - 1)
                _selectedTool = 0;
                else
                _selectedTool++;

        }

        public void DecreaseEquipIndex(InputAction.CallbackContext value)
        {
            if (_selectedTool <= 0)
                _selectedTool = equippableInventory.Length - 1;
            else
                _selectedTool--;
        }

        void SwitchEquipped()
        {
            selectedText.gameObject.SetActive(true);
            _equipIndex = 0;

            foreach (EquippableObject tool in equippableInventory)
            {
                if (_equipIndex == _selectedTool)
                {
                    selectedText.gameObject.SetActive(true);
                    selectedText.text = tool.toolName;
                    toolbarIcons[_equipIndex].color = selectedColor;
                    tool.ToggleEquip(true);
                }
                else
                {
                    toolbarIcons[_equipIndex].color = normalColor;
                    tool.ToggleEquip(false);
                }

                _equipIndex++;
            }
            
        }
    }
}