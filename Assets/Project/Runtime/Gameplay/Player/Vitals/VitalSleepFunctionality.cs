using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.Gameplay.Player
{
    public enum CurrentSleepState
    {
        NORMAL,
        RESTED,
        DROWSY,
        EXHAUSTED,
        ASLEEP
    }
    public class VitalSleepFunctionality : MonoBehaviour
    {
        public CurrentSleepState currentSleepState;
        [Header("Control Stats")] 
        public float restedThreshold;
        public float normalThreshhold;
        public float drowsyThreshold;
        public float exhuastedThreshold;
        public float sleepThreshold;
        public float blinkIntervalDrowsy;
        public float blinkIntervalExhausted;

        [Header("Buffs/Debuffs")] 
        public float movespeedBuff;
        public float negativeMoveSpeedBuff;

        [Header("Functionality")] 
        public Image blinkOverlay;

        public Image sleepOverlay;
        public Animator playerAnimator;

        #region Internal Variables

        private PlayerVitals _vitals;
        private BaseVital _sleep;
        private PlayerMovement _controller;
        private float _startingMovementSpeed;
        private bool _shouldBlink;
        private bool _isDrowsy = false;
        private Coroutine _blinkRoutine;
        private CurrentSleepState _lastSleepState;
        private const string SLEEP_TRIGGER = "Sleep";

        #endregion

        void Start()
        {
            _vitals = GetComponent<PlayerVitals>();
            _sleep = _vitals.sleepStat;

            _controller = GameManager.instance.playerManager._playerMovement;
            _startingMovementSpeed = _controller.walkSpeed;

        }

        private void Update()
        {

            if (_sleep.currentValue > restedThreshold)
            {
                ChangeState(CurrentSleepState.RESTED);
            }
            
            if (_sleep.currentValue <= normalThreshhold && currentSleepState == CurrentSleepState.RESTED)
            {
                ChangeState(CurrentSleepState.NORMAL);
            }
            
            if (_sleep.currentValue <= drowsyThreshold && currentSleepState == CurrentSleepState.NORMAL)
            {
                if(!_isDrowsy)
                    ChangeState(CurrentSleepState.DROWSY);
            }

            if (_sleep.currentValue <= exhuastedThreshold && currentSleepState == CurrentSleepState.DROWSY)
            {
                ChangeState(CurrentSleepState.EXHAUSTED);
            }
            
            if (_sleep.currentValue <= sleepThreshold && currentSleepState == CurrentSleepState.EXHAUSTED)
            {
                ChangeState(CurrentSleepState.ASLEEP);
            }
        }
        
        public void ChangeState(CurrentSleepState sleepState)
        {
            if(currentSleepState == sleepState)
                return;

            _lastSleepState = currentSleepState;
            currentSleepState = sleepState;
            
            switch (currentSleepState)
            {
                case CurrentSleepState.NORMAL:
                {
                    Debug.Log("Normal - Sleep");
                    PerformNormalFunctions();
                    break;
                }
                case CurrentSleepState.RESTED:
                {
                    Debug.Log("Rested");
                    PerformRestedFunctions();
                    break;
                }
                case CurrentSleepState.DROWSY:
                {
                    Debug.Log("Drowsy");
                    _isDrowsy = true;
                    PerformDrowsyFunctions();
                    break;
                }
                case CurrentSleepState.EXHAUSTED:
                {
                    Debug.Log("Exhausted");
                    PerformExhaustedFunctions();
                    break;
                }
                case CurrentSleepState.ASLEEP:
                {
                    Debug.Log("Asleep");
                    PerformSleepFunctions();
                    break;
                }
            }
        }

        private void PerformNormalFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You no longer feel well rested");
            _controller.walkSpeed = _startingMovementSpeed;
        }

        private void PerformRestedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel well rested");
            _controller.walkSpeed = _startingMovementSpeed + (_startingMovementSpeed * movespeedBuff);
        }
        
        private void PerformDrowsyFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel a little drowsy");
            if (_blinkRoutine == null)
            {
                _blinkRoutine = StartCoroutine(Blink(blinkIntervalDrowsy));
            }
        }
        
        private void PerformExhaustedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel exhausted");
            _controller.walkSpeed = _startingMovementSpeed - (_startingMovementSpeed * negativeMoveSpeedBuff);
            if (_blinkRoutine == null)
            {
                _blinkRoutine = StartCoroutine(Blink(blinkIntervalExhausted));
            }
            else
            {
                StopCoroutine(_blinkRoutine);
                StartCoroutine(Blink(blinkIntervalExhausted));
            }
        }
        
        private void PerformSleepFunctions()
        {
            StartCoroutine(Sleep());
        }

        IEnumerator Blink(float interval)
        {
            if (!_shouldBlink)
            {
                _shouldBlink = true;
            }
            
            while (_shouldBlink)
            {
                yield return new WaitForSeconds(interval);
                blinkOverlay.gameObject.SetActive(true);
            }
        }

        IEnumerator Sleep()
        {
            int randomValue = UnityEngine.Random.Range(1, 1);
            Debug.Log(randomValue.ToString());
            yield return new WaitForSeconds(1);
            
            if (randomValue == 1)
            {
                Debug.Log("Falling asleep");
                GameManager.instance.playerManager._playerController.enabled = false;
                GameManager.instance.cameraManager.enabled = false;
                sleepOverlay.gameObject.SetActive(true);
                playerAnimator.SetTrigger(SLEEP_TRIGGER);
                
                yield return new WaitForSeconds(5);
                
                Debug.Log("Waking Up");
                _sleep.AddValue(50);
                ChangeState(_lastSleepState);
                sleepOverlay.gameObject.SetActive(false);
                playerAnimator.SetTrigger(SLEEP_TRIGGER);
                GameManager.instance.playerManager._playerController.enabled = true;
                GameManager.instance.cameraManager.enabled = true;
                UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You collapsed from exhaustion. +50 Exhaustion for power nap");
                StopCoroutine(Sleep());
                yield return null;
            }
        }
    }

}