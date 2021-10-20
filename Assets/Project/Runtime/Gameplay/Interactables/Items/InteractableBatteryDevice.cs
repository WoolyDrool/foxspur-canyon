using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableBatteryDevice : MonoBehaviour
    {
        public float currentBatteryCharge;
        [SerializeField] private const float MaxBatteryCharge = 100;
        public float chargeDecreaseAmount;
        public float chargeDecreaseTick = 0.25f;
        public ChargeLevel currentChargeLevel;

        #region Internal Variables
        public enum ChargeLevel {FULL, ALMOSTFULL, HALFFULL, ALMOSTEMPTY, EMPTY }
        
        public delegate void ToggleDevice();
        
        public static event ToggleDevice OnToggle;
        
        private bool _deviceOn = false;

        #endregion

        public void PowerOnDevice()
        {
            if (currentChargeLevel != ChargeLevel.EMPTY)
            {
                _deviceOn = true;
            
                if (OnToggle != null)
                    OnToggle();
            
                StartCoroutine(DecreaseCharge());
            }
        }

        public void PowerDownDevice()
        {
            _deviceOn = false;
            
            if (OnToggle != null)
                OnToggle();
            
            StopCoroutine(DecreaseCharge());
        }

        public void Update()
        {
            switch (currentBatteryCharge)
            {
                case (100):
                {
                    currentChargeLevel = ChargeLevel.FULL;
                    break;
                }
                    
                case (75):
                {
                    currentChargeLevel = ChargeLevel.ALMOSTFULL;
                    break;
                }
                    
                case (50):
                {
                    currentChargeLevel = ChargeLevel.HALFFULL;
                    break;
                }
                    
                case (25):
                {
                    currentChargeLevel = ChargeLevel.ALMOSTEMPTY;
                    break;
                }
                    
                case (0):
                {
                    PowerDownDevice();
                    currentChargeLevel = ChargeLevel.EMPTY;
                    break;
                }
            }
        }

        public void AddBattery()
        {
            if (currentBatteryCharge <= MaxBatteryCharge)
            {
                float defecit = MaxBatteryCharge - currentBatteryCharge;
                currentBatteryCharge += defecit;
                currentChargeLevel = ChargeLevel.FULL;
                
                //Clamp sanity check
                Mathf.Clamp(currentBatteryCharge, 0, MaxBatteryCharge);
            }
        }

        IEnumerator DecreaseCharge()
        {
            while (_deviceOn)
            {
                currentBatteryCharge -= (Time.deltaTime * chargeDecreaseAmount);
                GameManager.instance.playerVitals.currentToolBattery = currentBatteryCharge;
                yield return new WaitForSeconds(chargeDecreaseTick);
            }
        }
    }

}