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
        public float fullThreshold;
        public float normalThreshold;
        public float dehydrationThreshold;

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

        }

        private void Update()
        {
            if (currentHydrationState != CurrentHydrationState.DEHYDRATED && _damageRoutine != null)
            {
                StopCoroutine(_damageRoutine);
            }
            
            if (_hydration.currentValue > fullThreshold)
            {
                ChangeState(CurrentHydrationState.HYDRATED);
            }

            if (_hydration.currentValue <= normalThreshold && currentHydrationState == CurrentHydrationState.HYDRATED)
            {
                if(!_isThirsty)
                    ChangeState(CurrentHydrationState.MILDDEHYDRATION);
            }
            
            if (_hydration.currentValue <= dehydrationThreshold && currentHydrationState == CurrentHydrationState.MILDDEHYDRATION)
            {
                ChangeState(CurrentHydrationState.DEHYDRATED);
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
        }

        private void PerformMildDehydratedFunctions()
        {
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel thirsty");
        }
        
        private void PerformDehydratedFunctions()
        {
            
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You are suffering from dehydration");
            if (_damageRoutine == null)
            {
                _damageRoutine = StartCoroutine(TakeHealthDamage());
            }
            else
            {
                StopCoroutine(_damageRoutine);
                _damageRoutine = StartCoroutine(TakeHealthDamage());
                
            }
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