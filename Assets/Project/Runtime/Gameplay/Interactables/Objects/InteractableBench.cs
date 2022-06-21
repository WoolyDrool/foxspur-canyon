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
        #region Public Variables
        
        public Transform tweenContainer;
        public Vector3 finalSitPosition;
        public Vector3 finalSitRotation;
        
        public Vector2 cameraBoundsWhenSitting;
        
        public LTBezierPath bezierPath;
        public float sitTime;
        public float sleepBenefit;
        public bool isSitting;
        
        #endregion
        
        #region Internal Variables

        private BaseVital _sleep;
        private PlayerMovement _movement;
        private PlayerController _controller;
        private CharacterController _characterController;
        private CameraMovement _cameraMovement;
        
        private StatModifier modifier;
        private LTDescr _tweener;
        
        [SerializeField] private Vector3 previousPlayerPosition;
        [SerializeField] private Quaternion previousPlayerRotation;
        
        private Vector2 _defaultCameraBounds;

        private Transform _playerTransform;
        
        #endregion
        
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

            finalSitPosition = tweenContainer.localPosition;
            Vector3 finalRotAsVec = new Vector3(tweenContainer.localRotation.x, tweenContainer.localRotation.y, tweenContainer.localRotation.z);
            finalSitRotation = finalRotAsVec;
        }

        void Update()
        {
            if (isSitting)
            {
                if (PlayerInputManager.i.interactAction.triggered)
                {
                    GetUp();
                }
            }
        }

        public void RestAtBench()
        {
           
            #region Sanity Checks

            //if (_sleep.currentValue > 90)
            //{
                //UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You don't feel tired");
                //return;
            //}

            if (GameManager.instance)
            {
                previousPlayerPosition = GameManager.instance.currentPlayerPosition;
                previousPlayerRotation = GameManager.instance.currentPlayerRotation;
                if(!_cameraMovement)
                    _cameraMovement = _controller.camera;

                if (_defaultCameraBounds.x < 0)
                {
                    _defaultCameraBounds = _cameraMovement.clampInDegrees;
                }
            
                if(!_playerTransform)
                    _playerTransform = GameManager.instance.playerManager.playerTransform;

                
            }
            else
            {
                return;
            }
            
            
            #endregion

            isSitting = true;
            _controller.ChangeStatus(Status.sitting);
            ToggleController(false);
            _sleep.AddModifier(modifier);
            
            //_sleep.AddValue(sleepBenefit);
            tweenContainer.SetParent(null);
            tweenContainer.SetPositionAndRotation(previousPlayerPosition, previousPlayerRotation);

            _playerTransform.SetParent(tweenContainer);

            tweenContainer.SetParent(transform);
            
            _tweener = LeanTween.moveLocal(tweenContainer.gameObject, finalSitPosition, sitTime);
            //_tweener = LeanTween.rotateLocal(tweenContainer.gameObject, finalSitRotation, sitTime);
            
            
        }

        public void GetUp()
        {
            Debug.Log("got up");
            _sleep.RemoveModifier(modifier);
            tweenContainer.SetParent(null);
            _tweener = LeanTween.move(tweenContainer.gameObject, previousPlayerPosition, sitTime);
            Vector3 prevRotAsVec = new Vector3(previousPlayerRotation.x, previousPlayerRotation.y, previousPlayerRotation.z);
            _tweener = LeanTween.rotate(tweenContainer.gameObject, prevRotAsVec, sitTime);

            StartCoroutine(GetUpWait());
            
            IEnumerator GetUpWait()
            {
                yield return new WaitForSeconds(sitTime);
                _playerTransform.SetParent(null);
                _controller.ChangeStatus(Status.idle);
                ToggleController(true);
                _cameraMovement.clampInDegrees = _defaultCameraBounds;
                isSitting = false;
                tweenContainer.SetParent(this.transform);
            }
        }
        
        
        void ToggleController(bool on)
        {
            _controller.enabled = on;
            _characterController.enabled = on;
            _movement.enabled = on;
            _cameraMovement.clampInDegrees = on ? cameraBoundsWhenSitting : _defaultCameraBounds;
        }
    }

}