 using System;
using System.Collections;
using System.Collections.Generic;
 using Project.Runtime.Gameplay.Inventory;
 using Project.Runtime.UI.Elements;
 using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableDoor : MonoBehaviour
    {
        public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
        public float openSpeedMultiplier = 2.0f; 
        public float doorOpenAngle = 90.0f;

        public Transform hinge;

        public bool locked = false;
        public Item key;

        public AudioClip openSound;
        public AudioClip closeSound;

        private PlayerInventory _inventory;

        private bool _open = false;
        private bool _enter = false;

        [SerializeField] private float _defaultRotationAngle;
        private float _currentRotationAngle;
        private float _openTime = 0;
        private AudioSource source;
        
        void Start()
        {
            _defaultRotationAngle = transform.localEulerAngles.y;
            _currentRotationAngle = transform.localEulerAngles.y;
            source = GetComponent<AudioSource>();

            _inventory = GameManager.instance.playerInventory;
        }

        void Update()
        {
            if (hinge != null)
            {
                if (_openTime < 1)
                {
                    _openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(_openTime);
                }

                hinge.localEulerAngles = new Vector3(hinge.localEulerAngles.x, Mathf.LerpAngle(_currentRotationAngle, _defaultRotationAngle + (_open ? doorOpenAngle : 0), _openTime), hinge.localEulerAngles.y);
            }
            else
            {
                return;
            }
            
        }

        public void InteractWithDoor()
        {
            if (!locked)
            {
                OpenDoor();
            }
            else
            {
                if (_inventory.inventoryItems.Contains(key))
                {
                    OpenDoor();
                }
                else
                {
                    UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "Need " + key.itemName);
                }
            }
            
        }

        void OpenDoor()
        {
            if (!_open)
            {
                source.clip = openSound;
            }
            else
            {
                source.clip = closeSound;
            }
            
            source.Play();
            _open = !_open;
            _currentRotationAngle = transform.localEulerAngles.z;
            _openTime = 0;
        }
    }
}
