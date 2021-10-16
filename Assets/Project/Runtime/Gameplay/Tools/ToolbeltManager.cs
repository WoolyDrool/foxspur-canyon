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

        private Quaternion _containerRot;
        private Quaternion _trackerRot;
        [SerializeField] private Vector3 _containerStartPos;
        [SerializeField] private Vector3 _containerPosCrouch;
        
        private int _equipIndex;
        private int _selectedTool = 0;
        private PlayerController _controller;
        private PlayerInput _input;
        private bool isCalled;
        [SerializeField] private bool canHandleMouseInput;

        #endregion
        

        private void Awake()
        {
            _controller = GetComponentInParent<PlayerController>();
            _input = GetComponentInParent<PlayerInput>();
            _containerStartPos = container.localPosition;
        }

        private void Start()
        {
            SwitchEquipped();
        }

        private void Update()
        {
            canHandleMouseInput = !GameManager.instance.controlsManager._showInventory;
            
            if (canHandleMouseInput)
            {
                int previousSelectedTool = _selectedTool;
                
                if (_input.mouseScroll > 0f)
                {
                    if (_selectedTool >= equippableInventory.Length - 1)
                        _selectedTool = 0;
                    else
                        _selectedTool++;
                }
                if (_input.mouseScroll < 0f)
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
                isCalled = true;
            }
            else
            {
                isCalled = false;
                container.localPosition = _containerStartPos;
            }
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