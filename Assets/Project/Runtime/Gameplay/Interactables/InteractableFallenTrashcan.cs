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
        void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
            _scoreManager = GetComponentInParent<InteractableTrailScoreManager>();
        }

        void Update()
        {
            if (_input.fixAction)
            {
                currentCompletion += 1 * Time.deltaTime;

                if (currentCompletion >= timeToComplete)
                {
                    CompleteAction();
                }
            }
        }

        private void CompleteAction()
        {
            _scoreManager.AddScore(1);
            fallenPrefab.gameObject.SetActive(false);
            completePrefab.SetActive(true);
        }
    }
}
