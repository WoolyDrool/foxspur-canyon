using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableFallenTrashcan : MonoBehaviour
    {
        public GameObject completePrefab;
        public Transform fallenPrefab;
        public float timeToComplete;
        private float currentCompletion = 0;
        public Image progressBar;
        private PlayerInput _input;
        private InteractableTrailScoreManager _scoreManager;
        private HudInteractableController _hudInteractable;
        private Interactable _interactable;
        private bool _canFix = true;
        void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
            _scoreManager = GetComponentInParent<InteractableTrailScoreManager>();
            _hudInteractable = FindObjectOfType<HudInteractableController>();
            _interactable = GetComponent<Interactable>();
            completePrefab.SetActive(false);
            fallenPrefab.gameObject.SetActive(true);
        }

        void Update()
        {
            if (_hudInteractable.currentInteractable == this._interactable)
            {
                if (_input.fixAction)
                {
                    currentCompletion += 1 * Time.deltaTime;
                    progressBar.fillAmount = (currentCompletion / timeToComplete);

                    if (currentCompletion >= timeToComplete && _canFix)
                    {
                        CompleteAction();
                    }
                }
            }
            else
            {
                return;
            }
            
        }

        private void CompleteAction()
        {
            progressBar.fillAmount -= 1;
            _canFix = false;
            _scoreManager.AddScore(1);
            fallenPrefab.gameObject.SetActive(false);
            completePrefab.SetActive(true);
        }
    }
}
