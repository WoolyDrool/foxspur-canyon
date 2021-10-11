using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Player;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsHands : EquippableObject
    {
        public GameObject objectToHold;
        private const string GRAB_TRIGGER = "Grab";
        public float grabRange = 5;
        public LayerMask grabLayers;
        public Transform dropAndGrabPoint;
        public float throwForce;
        public GameObject interactor;

        [SerializeField] private Transform playerCamera;
        private Ray _ray;
        private RaycastHit _rayHit;
        private Rigidbody tempRigidbodyComponent;
        private bool grabbingObject = false;
       

        public override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            playerCamera = FindObjectOfType<CameraMovement>().transform;
        }

        void Update()
        {
            if (Physics.Raycast(playerCamera.transform.position,
                playerCamera.transform.TransformDirection(Vector3.forward), out _rayHit, grabRange, grabLayers))
            {
                if (_input.grabbing && objectToHold == null)
                {
                    if (_rayHit.collider.TryGetComponent(out InteractableThrowableItem throwableItem))
                    {
                        objectToHold = _rayHit.collider.gameObject;
                        objectAnimator.SetTrigger(GRAB_TRIGGER);
                        grabbingObject = true;
                    }
                }
            }

            if (_input.grabbing && !grabbingObject)
            {
                if (objectToHold)
                {
                    ThrowObject();
                }
            }
            
        }

        public void ThrowObject()
        {
            if (objectToHold.TryGetComponent(out InteractableThrowableItem throwableItem))
            {
                interactor.SetActive(true);
                throwableItem.isBeingHeld = false;
                objectToHold.transform.parent = null;
                objectToHold.transform.position = dropAndGrabPoint.position;
                tempRigidbodyComponent.isKinematic = false;
                tempRigidbodyComponent.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
                tempRigidbodyComponent = null;
                objectToHold = null;
            }
        }

        public void GrabObject()
        {
            if (objectToHold.TryGetComponent(out InteractableThrowableItem throwableItem))
            {
                interactor.SetActive(false);
                tempRigidbodyComponent = objectToHold.GetComponent<Rigidbody>();
                tempRigidbodyComponent.isKinematic = true;
                throwableItem.isBeingHeld = true;
                objectAnimator.SetTrigger(GRAB_TRIGGER);
                grabbingObject = false;
                objectToHold.transform.parent = dropAndGrabPoint;
                objectToHold.transform.position = dropAndGrabPoint.position;
            }
        }
    }
}
