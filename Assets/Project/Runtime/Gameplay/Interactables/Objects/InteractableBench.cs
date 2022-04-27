using System.Collections;
using System.Collections.Generic;
using System.IO;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.UI.Elements;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableBench : MonoBehaviour
    {
        private BaseVital _sleep;
        private PlayerMovement _movement;
        private PlayerController _controller;
        private CharacterController _characterController;
        private CameraMovement _cameraMovement;
        public Vector2 cameraBoundsWhenSitting;
        private Vector2 _defaultCameraBounds;
        public Transform playerContainer;
        private Transform _playerTransform;
        public Vector3 sitPosition;
        public Vector3 sitRotation;
        [SerializeField] private Vector3 previousPosition;
        [SerializeField] private Quaternion previousRotation;
        private LTDescr _tweener;
        public LTBezierPath bezierPath;
        public float sitTime;
        public float sleepBenefit;
        public bool isSitting;
        private StatModifier modifier;
        void Start()
        {
            _sleep = GameManager.instance.playerVitals.sleepStat;
            _movement = GameManager.instance.playerManager._playerMovement;
            _controller = GameManager.instance.playerManager._playerController;
            _characterController = _movement.GetComponent<CharacterController>();
            _playerTransform = GameManager.instance.playerManager.playerTransform;
            _cameraMovement = _controller.camera;
            _defaultCameraBounds = _cameraMovement.clampInDegrees;
            modifier = new StatModifier(_sleep, ModifierType.BUFF, OperationType.ADD, sleepBenefit, 0);
        }

        void Update()
        {
            if (isSitting)
            {
                if (PlayerInputManager.i.interact)
                {
                    GetUp();
                }
            }
        }

        public void RestAtBench()
        {
            #region Sanity Checks

            if (_sleep.currentValue > 90)
            {
                UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You don't feel tired");
                return;
            }
            
            if(!_cameraMovement)
                _cameraMovement = _controller.camera;

            if (_defaultCameraBounds.x < 0)
            {
                _defaultCameraBounds = _cameraMovement.clampInDegrees;
            }
            
            if(!_playerTransform)
                _playerTransform = GameManager.instance.playerManager.playerTransform;

            #endregion
            
            //Get Players Initial Rotation
            previousPosition = _playerTransform.localPosition;
            previousRotation = _playerTransform.localRotation;
            
            
            //Set the containers position and rotation to that of the player
            playerContainer.parent = null;
            playerContainer.position = _playerTransform.position;
            playerContainer.rotation = _playerTransform.rotation;
            
            //Parent the player to the container
            _playerTransform.SetParent(playerContainer);
            playerContainer.SetParent(this.transform);
            
            //Apply Rotation
            _playerTransform.SetPositionAndRotation(playerContainer.position, playerContainer.rotation);
            
            //Disable the player
            _controller.ChangeStatus(Status.sitting);
            ToggleController(false);
            _cameraMovement.clampInDegrees = cameraBoundsWhenSitting;
            
            //Lerp the playerContainer to the sitting position
            _tweener = LeanTween.moveLocal(playerContainer.gameObject, sitPosition, sitTime);
            _tweener = LeanTween.rotateLocal(playerContainer.gameObject, sitRotation, sitTime);
            
            //_sleep.AddValue(sleepBenefit);
            isSitting = true;
            _sleep.AddModifier(modifier);
        }

        public void GetUp()
        {
            _sleep.RemoveModifier(modifier);
            playerContainer.SetParent(null);
            _tweener = LeanTween.move(playerContainer.gameObject, previousPosition, sitTime);
            Vector3 prevRotAsVec = new Vector3(previousRotation.x, previousRotation.y, previousRotation.z);
            _tweener = LeanTween.rotate(playerContainer.gameObject, prevRotAsVec, sitTime);

            StartCoroutine(GetUpWait());
            
            IEnumerator GetUpWait()
            {
                yield return new WaitForSeconds(sitTime);
                _playerTransform.SetParent(null);
                _controller.ChangeStatus(Status.idle);
                ToggleController(true);
                _cameraMovement.clampInDegrees = _defaultCameraBounds;
                isSitting = false;
                playerContainer.SetParent(this.transform);
            }
        }
        
        
        void ToggleController(bool on)
        {
            _controller.enabled = on;
            _characterController.enabled = on;
            _movement.enabled = on;
        }
    }

}