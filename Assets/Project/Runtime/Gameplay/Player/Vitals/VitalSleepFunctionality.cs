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
        [SerializeField] private bool _shouldBlink;
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
            InvokeRepeating(nameof(CheckCurrentSleep), 0, 1);
        }
        
        //DEBUG_COMMAND
        public void ForceExhaustion()
        {
            _sleep.currentValue = 0;
            ChangeState(CurrentSleepState.EXHAUSTED);
        }

        public void ForceBlinking()
        {
            _sleep.currentValue = drowsyThreshold;
            ChangeState(CurrentSleepState.DROWSY);
        }

        private void Update()
        private void CheckCurrentSleep()
        {

            if (_sleep.CheckCurrentValue(true) > restedThreshold)
            {
                ChangeState(CurrentSleepState.RESTED);
            }
            
            if (_sleep.CheckCurrentValue(true) < restedThreshold && currentSleepState == CurrentSleepState.RESTED)
            {
                ChangeState(CurrentSleepState.NORMAL);
            }
            
            if (_sleep.CheckCurrentValue(true)  <= normalThreshhold && currentSleepState == CurrentSleepState.RESTED)
            {
                ChangeState(CurrentSleepState.NORMAL);
            }
            
            if (_sleep.CheckCurrentValue(true)  <= drowsyThreshold && currentSleepState == CurrentSleepState.NORMAL)
            {
                if(!_isDrowsy)
                    ChangeState(CurrentSleepState.DROWSY);
            }

            if (_sleep.CheckCurrentValue(true)  <= exhuastedThreshold && currentSleepState == CurrentSleepState.DROWSY)
            {
                ChangeState(CurrentSleepState.EXHAUSTED);
            }
            
            if (_sleep.CheckCurrentValue(true)  <= sleepThreshold && currentSleepState == CurrentSleepState.EXHAUSTED)
            {
                ChangeState(CurrentSleepState.ASLEEP);
            }

            if (_sleep.CheckCurrentValue(true) > exhuastedThreshold && currentSleepState == CurrentSleepState.EXHAUSTED)
            {
                ChangeState(CurrentSleepState.DROWSY);
            }
            
            if (_sleep.CheckCurrentValue(true) > drowsyThreshold && currentSleepState == CurrentSleepState.DROWSY)
            {
                ChangeState(CurrentSleepState.NORMAL);
            }
            
            if (currentSleepState == CurrentSleepState.EXHAUSTED)
            {
                if (_sleep.currentValue > exhuastedThreshold)
                {
                    if (_blinkRoutine != null)
                    {
                        StopBlinkRoutine();
                    }
                    else
                    {
                        return;
                    }
                }
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
            _shouldBlink = false;
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You no longer feel well rested");
            _controller.walkSpeed = _startingMovementSpeed;
            if (_blinkRoutine != null)
            {
                StopBlinkRoutine();
            }
        }

        private void PerformRestedFunctions()
        {
            _shouldBlink = false;
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel well rested");
            _controller.walkSpeed = _startingMovementSpeed + (_startingMovementSpeed * movespeedBuff);
            if (_blinkRoutine != null)
            {
                StopBlinkRoutine();
            }
        }
        
        private void PerformDrowsyFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel a little drowsy");
            if (_blinkRoutine == null)
            {
                _shouldBlink = true;
                _blinkRoutine = StartCoroutine(Blink(blinkIntervalDrowsy));
            }
        }
        
        private void PerformExhaustedFunctions()
        {
            _shouldBlink = true;
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You feel exhausted");
            _controller.walkSpeed = _startingMovementSpeed - (_startingMovementSpeed * negativeMoveSpeedBuff);
            if (_blinkRoutine == null)
            {
                _blinkRoutine = StartCoroutine(Blink(blinkIntervalExhausted));
            }
        }
        
        private void PerformSleepFunctions()
        {
            StartCoroutine(Sleep());
        }

        private void StopBlinkRoutine()
        {
            _blinkRoutine = null;
            StopCoroutine(Blink(0));
            _shouldBlink = false;
        }

        IEnumerator Blink(float interval)
        {
            if (_shouldBlink)
            {
                while (_shouldBlink)
                {
                    yield return new WaitForSeconds(interval);
                    blinkOverlay.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Not blinking");
                StopCoroutine(Blink(0));
                _shouldBlink = false;
                blinkOverlay.gameObject.SetActive(false);
            }
        }

        IEnumerator Sleep()
        {
            Debug.Log("Falling asleep");
            GameManager.instance.playerManager._playerController.enabled = false;
            GameManager.instance.cameraManager.enabled = false;
            sleepOverlay.gameObject.SetActive(true);
            playerAnimator.SetTrigger(SLEEP_TRIGGER);
                
            yield return new WaitForSeconds(5);
                
            Debug.Log("Waking Up");
            _sleep.AddValue(drowsyThreshold);
                
            sleepOverlay.gameObject.SetActive(false);
            playerAnimator.SetTrigger(SLEEP_TRIGGER);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You collapsed from exhaustion. +50 Exhaustion for power nap");
            yield return new WaitForSeconds(3);
            ChangeState(CurrentSleepState.DROWSY);
            GameManager.instance.playerManager._playerController.enabled = true;
            GameManager.instance.cameraManager.enabled = true;
            StopCoroutine(Sleep());
            yield return null;
        }
    }

}