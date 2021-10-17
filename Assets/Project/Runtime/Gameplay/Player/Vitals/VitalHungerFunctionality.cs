using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.Gameplay.Player
{
    public enum CurrentHungerState
    {
        FULL,
        NORMAL,
        HUNGRY,
        STARVING
    }

    public class VitalHungerFunctionality : MonoBehaviour
    {
        public CurrentHungerState currentHungerState;
        [Header("Control Stats")] 
        public float fullThreshold;
        public float normalThreshold;
        public float hungryThreshold;
        public float starvingThreshold;

        [Header("Buffs")] 
        public float buffedHydrationTick;
        public float debuffHydrationTick;
        public float buffedSleepTick;
        public float debuffSleepTick;
        
        #region Internal Variables

        private PlayerVitals _vitals;
        private BaseVital _hunger;
        private bool _isHungry = false;
        private CurrentHungerState _lastHungerState;

        #endregion

        void Start()
        {
            _vitals = GetComponent<PlayerVitals>();
            _hunger = _vitals.hungerStat;

        }
        
        //DEBUG_COMMAND
        public void ForceStarvation()
        {
            _hunger.currentValue = 0;
            ChangeState(CurrentHungerState.STARVING);
        }

        private void Update()
        {
            if (_hunger.currentValue > fullThreshold)
            {
                ChangeState(CurrentHungerState.FULL);
            }

            if (_hunger.currentValue <= normalThreshold && currentHungerState == CurrentHungerState.FULL)
            {
                ChangeState(CurrentHungerState.NORMAL);
            }

            if (_hunger.currentValue <= hungryThreshold && currentHungerState == CurrentHungerState.NORMAL)
            {
                if (!_isHungry)
                    ChangeState(CurrentHungerState.HUNGRY);
            }

            if (_hunger.currentValue <= starvingThreshold && currentHungerState == CurrentHungerState.HUNGRY)
            {
                ChangeState(CurrentHungerState.STARVING);
            }
        }

        public void ChangeState(CurrentHungerState hungerState)
        {
            if (currentHungerState == hungerState)
                return;

            _lastHungerState = currentHungerState;
            currentHungerState = hungerState;

            switch (currentHungerState)
            {
                case CurrentHungerState.NORMAL:
                {
                    Debug.Log("Normal - Hunger");
                    PerformNormalFunctions();
                    break;
                }
                case CurrentHungerState.FULL:
                {
                    Debug.Log("Full");
                    PerformFullFunctions();
                    break;
                }
                case CurrentHungerState.HUNGRY:
                {
                    Debug.Log("Hungry");
                    _isHungry = true;
                    PerformHungryFunctions();
                    break;
                }
                case CurrentHungerState.STARVING:
                {
                    Debug.Log("Starving");
                    PerformStarvingFunctions();
                    break;
                }
            }
        }

        private void PerformNormalFunctions()
        {
            EventManager.TriggerEvent("DisableShake", null);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You no longer feel full");
            _vitals.sleepStat.ReturnTickValue();
            _vitals.hydrationStat.ReturnTickValue();
        }

        private void PerformFullFunctions()
        {
            EventManager.TriggerEvent("DisableShake", null);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel full");
            _vitals.sleepStat.AdjustTickValue(buffedSleepTick);
            _vitals.hydrationStat.AdjustTickValue(buffedHydrationTick);
        }

        private void PerformHungryFunctions()
        {
            EventManager.TriggerEvent("DisableShake", null);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel a little hungry");
        }

        private void PerformStarvingFunctions()
        {
            EventManager.TriggerEvent("EnableShake", null);
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "You feel incredibly hungry");
            _vitals.sleepStat.AdjustTickValue(debuffSleepTick);
            _vitals.hydrationStat.AdjustTickValue(debuffHydrationTick);
        }
    }

}