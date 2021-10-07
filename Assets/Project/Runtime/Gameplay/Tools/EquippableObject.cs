using System;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class EquippableObject : MonoBehaviour
    {
        public string toolName;
        public Animator objectAnimator;
        public bool isCurrentlyEquipped = false;

        public virtual void Awake()
        {
            objectAnimator = GetComponent<Animator>();
            if (toolName == null)
                toolName = "Debug";
        }

        public virtual void ToggleEquip(bool on)
        {
            if (on)
            {
                isCurrentlyEquipped = true;
                gameObject.SetActive(true);
            }
            else
            {
                isCurrentlyEquipped = false;
                gameObject.SetActive(false);
            }
        }
        
    }
}