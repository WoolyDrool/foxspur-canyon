using System;
using Project.Runtime.Gameplay.Interactables;
using UnityEngine;

namespace Project.Runtime.Environment
{
    public class TrailStartTrigger : MonoBehaviour
    {
        public RuntimeTrailManager trailManager;

        private void Awake()
        {
            if (!trailManager)
            {
                Debug.LogError("No RuntimeTrailManager assigned");
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                trailManager.BeginTrail();
                this.gameObject.SetActive(false);
            }
        }
    }
}