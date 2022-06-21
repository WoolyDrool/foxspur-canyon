using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Global;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.Gameplay.Player
{
    public enum CurrentHydrationState
    {
        HYDRATED,
        MILDDEHYDRATION,
        DEHYDRATED
    }
    public class VitalHydrationFunctionality : MonoBehaviour
    {
        public CurrentHydrationState currentHydrationState;
        [Header("Control Stats")] 
        public int fullThreshold;
        public int normalThreshold;
        public int dehydrationThreshold;

        [Header("Buffs")]
        public float debuffSleepTick;

        public float healthDamageTick;
        
        #region Internal Variables

        private PlayerVitals _vitals;
        private VitalHealthFunctionality _health;
        private BaseVital _hydration;
        private bool _isThirsty = false;
        [SerializeField] private bool _shouldTakeTickDamage;
        private Coroutine _damageRoutine;
        private CurrentHydrationState _lastHungerState;

        #endregion

        private void Awake()
        {
           
        }

        void Start()
        {
            _vitals = GetComponent<PlayerVitals>();
            _hydration = _vitals.hydrationStat;
            _health = GetComponent<VitalHealthFunctionality>();
            InvokeRepeating(nameof(CheckCurrentHydration), 0, 1);
        }
        
        //DEBUG_COMMAND
        public void ForceDehydration()
        {
            _hydration.currentValue = 0;
            ChangeState(CurrentHydrationState.DEHYDRATED);
        }
        

        private void CheckCurrentHydration()
        {
            int currentHydration = (int)_hydration.currentValue;
            
            if (_hydration.CheckCurrentValue(true) > fullThreshold)
            {
                ChangeState(CurrentHydrationState.HYDRATED);
            }

            if (_hydration.CheckCurrentValue(true) <= normalThreshold && currentHydrationState == CurrentHydrationState.HYDRATED)
            {
                if(!_isThirsty)
                    ChangeState(CurrentHydrationState.MILDDEHYDRATION);
            }
            
            if (_hydration.CheckCurrentValue(true) <= dehydrationThreshold && currentHydrationState == CurrentHydrationState.MILDDEHYDRATION)
            {
                ChangeState(CurrentHydrationState.DEHYDRATED);
            }

            if (_hydration.CheckCurrentValue(true) > dehydrationThreshold &&
                currentHydrationState == CurrentHydrationState.DEHYDRATED)
            {
                ChangeState(CurrentHydrationState.MILDDEHYDRATION);
            }

            if (currentHydrationState == CurrentHydrationState.DEHYDRATED)
            {
                if (_hydration.currentValue >= dehydrationThreshold)
                {
                    if (_damageRoutine != null)
                    {
                        StopHealthDamage();
                    }
                }
            }
        }

        public void ChangeState(CurrentHydrationState hungerState)
        {
            if (currentHydrationState == hungerState)
                return;
            
            _lastHungerState = currentHydrationState;
            currentHydrationState = hungerState;

            switch (currentHydrationState)
            {
                case CurrentHydrationState.HYDRATED:
                {
                    Debug.Log("Hydrated");
                    PerformHydratedFunctions();
                    break;
                }
                case CurrentHydrationState.MILDDEHYDRATION:
                {
                    Debug.Log("Mildly Dehydrated");
                    _isThirsty = true;
                    PerformMildDehydratedFunctions();
                    break;
                }
                case CurrentHydrationState.DEHYDRATED:
                {
                    Debug.Log("Dehydrated");
                    _shouldTakeTickDamage = true;
                    PerformDehydratedFunctions();
                    break;
                }
            }
        }

        private void PerformHydratedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel hydrated");
            if(_damageRoutine != null)
                StopHealthDamage();
        }

        private void PerformMildDehydratedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel thirsty");
            if(_damageRoutine != null)
                StopHealthDamage();
        }
        
        private void PerformDehydratedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You are suffering from dehydration");
            if (_damageRoutine == null)
            {
                _damageRoutine = StartCoroutine(TakeHealthDamage());
            }
        }

        private void StopHealthDamage()
        {
            Debug.Log("Stopping tick damage");
            _damageRoutine = null;
            StopCoroutine(TakeHealthDamage());
            _shouldTakeTickDamage = false;
        }

        IEnumerator TakeHealthDamage()
        {
            while (_shouldTakeTickDamage)
            {
                _health.TakeDamage(healthDamageTick);
                yield return new WaitForSeconds(5);
                
                yield return null;
            }
        }
    }
}