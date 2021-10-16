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
        [Header("Door Info")]
        public bool locked = false;
        public Item key;
        
        [Header("Animation")]
        public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
        public float openSpeedMultiplier = 2.0f; 
        public float doorOpenAngle = 90.0f;

        [Header("Sounds")]
        public AudioClip openSound;
        public AudioClip closeSound;
        public AudioClip lockedSound;
        
        public Transform objectToRotate;
        private PlayerInventory _inventory;
        
        #region Internal Variables

        private bool _open = false;
        private float _defaultRotationAngle;
        private float _currentRotationAngle;
        private float _openTime = 0;
        private AudioSource source;

        #endregion
        
        void Start()
        {
            source = GetComponent<AudioSource>();
            _inventory = GameManager.instance.playerInventory;
            
            try
            {
                _defaultRotationAngle = objectToRotate.localEulerAngles.y;
                _currentRotationAngle = objectToRotate.localEulerAngles.y;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        void Update()
        {
            if (objectToRotate)
            {
                if (_openTime < 1)
                {
                    _openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(_openTime);
                }

                objectToRotate.localEulerAngles = new Vector3(objectToRotate.localEulerAngles.x, Mathf.LerpAngle(_currentRotationAngle, _defaultRotationAngle + (_open ? doorOpenAngle : 0), _openTime),objectToRotate.localEulerAngles.z);
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
                    source.PlayOneShot(lockedSound);
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
            _currentRotationAngle = objectToRotate.localEulerAngles.y;
            _openTime = 0;
        }
    }
}
