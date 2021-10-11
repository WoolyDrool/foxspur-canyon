using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class HudInteractableController : MonoBehaviour
    {
        public LayerMask collisionLayer;
        public LayerMask interactionLayer;

        public Interactable currentInteractable;
        
        PlayerInput input;
        HudInteraction ui;
        Transform mainCamera;

        private RaycastHit hit;

        private void Start()
        {
            input = GetComponentInParent<PlayerInput>();
            mainCamera = Camera.main.GetComponent<CameraMovement>().transform;

            ui = FindObjectOfType<HudInteraction>();
            if (ui == null) Debug.LogError("InteractionControllerUI not found, please add PlayerUI prefab");
            if (ui) ui.SetCode(input.interactKey.ToString());
        }

        void Update()
        {
            Interactable interactWith = null;

            //First send a ray out forwards to hit anything
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 30f))
            {
                Debug.DrawRay(mainCamera.position, mainCamera.forward * 30f, Color.red);
                //Get the distance then send another ray for only the interaction layer using that distance
                
                
            }
            float dis = Vector3.Distance(mainCamera.position, hit.point);
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out var interact, dis, interactionLayer))
            {
                Interactable inFront = interact.transform.GetComponent<Interactable>();
                currentInteractable = inFront;
                if (inFront == null) return;
                if (dis > inFront.interactRange + 0.5f)
                    inFront = null;
                interactWith = inFront; //Set interactWith to the one we hit

                if (interactWith != null)
                {
                    if (ui) ui.UpdateInteract(interactWith.description);
                    if (input.interact)
                        interactWith.Interact();
                }
            }
            else
            {
                currentInteractable = null;
            }

            if (ui) ui.InteractableSelected(interactWith != null);
        }

        private void OnDisable()
        {
            if (ui) ui.InteractableSelected(false);
        }
    }
}
