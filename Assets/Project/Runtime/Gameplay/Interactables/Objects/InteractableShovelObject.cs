using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableShovelObject : MonoBehaviour
    {
        public int swingsToComplete;
        public int currentSwings;
        public Transform pileObject;
        private AudioSource _source;
        public AudioClip completionClip;

        private InteractableTrailScoreManager _scoreManager;
        public Vector3[] positions;
        void Start()
        {
            _scoreManager = GetComponentInParent<InteractableTrailScoreManager>();
            _source = GetComponent<AudioSource>();
        }

        void Update()
        {

        }

        public void ProcessDig()
        {
            currentSwings++;
            ChangeState();
        }

        void ChangeState()
        {
            if (currentSwings == 1)
            {
                pileObject.localPosition = positions[0];
            }

            if (currentSwings == 2)
            {
                pileObject.localPosition = positions[1];
            }

            if (currentSwings == 3)
            {
                pileObject.localPosition = positions[2];
            }

            if (currentSwings == 4)
            {
                _scoreManager.AddScore(1);
                pileObject.gameObject.SetActive(false);
                _source.PlayOneShot(completionClip);
                Destroy(gameObject, 5f);
            }
        }
    }

}