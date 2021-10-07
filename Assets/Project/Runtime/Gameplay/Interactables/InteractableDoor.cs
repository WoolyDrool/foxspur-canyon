using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableDoor : MonoBehaviour
    {
        public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
        public float openSpeedMultiplier = 2.0f; 
        public float doorOpenAngle = 90.0f;

        public AudioClip openSound;
        public AudioClip closeSound;

        private bool _open = false;
        private bool _enter = false;

        private float _defaultRotationAngle;
        private float _currentRotationAngle;
        private float _openTime = 0;
        private AudioSource source;
        
        void Start()
        {
            _defaultRotationAngle = transform.localEulerAngles.y;
            _currentRotationAngle = transform.localEulerAngles.y;
            source = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (_openTime < 1)
            {
                _openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(_openTime);
            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(_currentRotationAngle, _defaultRotationAngle + (_open ? doorOpenAngle : 0), _openTime), transform.localEulerAngles.z);

            if (Input.GetKeyDown(KeyCode.F) && _enter)
            {
                _open = !_open;
                _currentRotationAngle = transform.localEulerAngles.y;
                _openTime = 0;
            }
        }

        public void InteractWithDoor()
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
            _currentRotationAngle = transform.localEulerAngles.y;
            _openTime = 0;
        }
    }
}
