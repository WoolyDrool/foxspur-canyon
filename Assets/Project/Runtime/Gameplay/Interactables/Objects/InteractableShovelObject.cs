using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableShovelObject : MonoBehaviour
    {
        public int minSwingsToComplete;
        public int maxSwingsToComplete;
        private int _actualSwingsToComplete;
        public int currentSwings;
        private int _position;
        public Transform pileObject;
        private AudioSource _source;
        public AudioClip completionClip;

        private InteractableTrailScoreManager _scoreManager;
        public Vector3[] positions;
        void Start()
        {
            _scoreManager = GetComponentInParent<InteractableTrailScoreManager>();
            _source = GetComponent<AudioSource>();
            _actualSwingsToComplete = Random.Range(minSwingsToComplete, maxSwingsToComplete+1);
        }

        void Update()
        {

        }

        public void ProcessDig()
        {
            currentSwings++;
            ChangeState();
            if (_position < positions.Length-1)
            {
                _position++;
                pileObject.localPosition = positions[_position];
            }
        }

        void ChangeState()
        {
            if (currentSwings == _actualSwingsToComplete)
            {
                _scoreManager.AddScore(1);
                pileObject.gameObject.SetActive(false);
                _source.PlayOneShot(completionClip);
                Destroy(gameObject, 5f);
            }
        }
    }

}