using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableFallenTrashcan : MonoBehaviour
    {
        [Header("Models")]
        public GameObject completePrefab;
        public Transform fallenPrefab;
        public BoxCollider hitbox;
        
        [Header("Controls")]
        public float timeToComplete;
        private float currentCompletion = 0;
        
        [Header("UI")]
        public Image progressBar;

        #region Internal Variables

        private PlayerInputManager _inputManager;
        private RuntimeTrailManager _manager;
        private HudInteractableController _hudInteractable;
        private Interactable _interactable;
        private bool _canFix = true;

        #endregion

        void Start()
        {
            //Get components
            _inputManager = GameManager.instance.inputManager;
            _manager = FindObjectOfType<RuntimeTrailManager>();
            _hudInteractable = GameManager.instance.hudInteractionController;
            _interactable = GetComponent<Interactable>();
            
            //Set model status
            completePrefab.SetActive(false);
            fallenPrefab.gameObject.SetActive(true);
        }

        void Update()
        {
            if (_hudInteractable.currentInteractable == this._interactable)
            {
                if (_inputManager.holdInteract && _canFix)
                {
                    currentCompletion += 1 * Time.deltaTime;
                    progressBar.fillAmount = (currentCompletion / timeToComplete);

                    if (currentCompletion >= timeToComplete)
                    {
                        progressBar.fillAmount = 0;
                        CompleteAction();
                    }
                }
                else
                {
                    currentCompletion = 0;
                    progressBar.fillAmount = 0;
                }
            }
            else
            {
                return;
            }
        }

        private void CompleteAction()
        {
            _canFix = false;
            _manager.AddScore(1);
            hitbox.enabled = false;
            _interactable.enabled = false;
            fallenPrefab.gameObject.SetActive(false);
            completePrefab.SetActive(true);
        }
    }
}
