using System;
using System.Collections;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class EquippableObject : MonoBehaviour
    {
        public string toolName;
        public Animator objectAnimator;
        public bool isCurrentlyEquipped = false;
        public PlayerInputManager inputManager;
        public bool hasCooldown = false;
        public float cooldownTime = 1;
        public bool canUse = true;
        public virtual void Awake()
        {
            objectAnimator = GetComponent<Animator>();
            inputManager = GetComponentInParent<PlayerInputManager>();
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

        public void ProcessCooldown()
        {
            canUse = false;
            StartCoroutine(Cooldown());
            
            IEnumerator Cooldown()
            {
                yield return new WaitForSeconds(cooldownTime);
                canUse = true;
                yield break;
            }
        }

    }
}