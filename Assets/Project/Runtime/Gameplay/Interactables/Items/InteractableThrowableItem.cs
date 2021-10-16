using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class InteractableThrowableItem : MonoBehaviour
    {
        public int amount;
        public bool isBeingHeld;
        public bool hasBeenDeposited;

        public string customDescription;
        public Interactable interaction;
        void Start()
        {
            if (interaction != null)
            {
                interaction.description = customDescription + "(" + amount.ToString() + ")";
            }
        }

        void Update()
        {

        }
    }

}