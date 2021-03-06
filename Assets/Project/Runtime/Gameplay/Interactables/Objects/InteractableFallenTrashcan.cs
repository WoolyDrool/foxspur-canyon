using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private PlayerInput _input;
        private InteractableTrailScoreManager _scoreManager;
        private HudInteractableController _hudInteractable;
        private Interactable _interactable;
        private bool _canFix = true;

        #endregion
        
        void Start()
        {
            //Get components
            _input = FindObjectOfType<PlayerInput>();
            _scoreManager = GetComponentInParent<InteractableTrailScoreManager>();
            _hudInteractable = FindObjectOfType<HudInteractableController>();
            _interactable = GetComponent<Interactable>();
            
            //Set model status
            completePrefab.SetActive(false);
            fallenPrefab.gameObject.SetActive(true);
        }

        void Update()
        {
            if (_hudInteractable.currentInteractable == this._interactable)
            {
                if (_input.hold_interact && _canFix)
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
            _scoreManager.AddScore(1);
            hitbox.enabled = false;
            _interactable.enabled = false;
            fallenPrefab.gameObject.SetActive(false);
            completePrefab.SetActive(true);
        }
    }
}
