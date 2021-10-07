using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsHeadlamp : MonoBehaviour
    {
        public GameObject Light;
        public Light actualLight;

        public AudioSource soundSource;

        public AudioSource hum;

        public AudioClip onSound;

        public AudioClip offSound;

        public bool headlampOn;

        #region Internal Variables

        private InteractableBatteryDevice _batteryDevice;
        
        private float startingIntensity;
        private float dimIntensity;

        #endregion
        

        // Start is called before the first frame update
        void Start()
        {
            _batteryDevice = GetComponent<InteractableBatteryDevice>();
            startingIntensity = actualLight.intensity;
            dimIntensity = startingIntensity / 2;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!headlampOn)
                {
                    ToggleHeadLamp();
                    headlampOn = true;
                }
                else
                {
                    ToggleHeadLamp();
                    headlampOn = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && headlampOn)
            {
                LoadBattery();
            }
            
            
        }


        void ToggleHeadLamp()
        {
            Light.SetActive(!headlampOn);
            AudioClip soundClip = headlampOn ? onSound : offSound;
            soundSource.clip = soundClip;
            soundSource.Play();
            
            if (_batteryDevice.currentChargeLevel == InteractableBatteryDevice.ChargeLevel.HALFFULL)
            {
                actualLight.intensity = dimIntensity;
            }
            
            if (headlampOn)
            {
                _batteryDevice.PowerDownDevice();
                hum.Stop();
            }
            else
            {
                _batteryDevice.PowerOnDevice();
                hum.Play();
                
            }
        }

        void LoadBattery()
        {
            if (GameManager.instance.playerInventory.currentBatteries >=  1)
            {
                Debug.Log("Loaded battery");
                actualLight.intensity = startingIntensity;
                GameManager.instance.playerInventory.currentBatteries--;
                _batteryDevice.AddBattery();
            }
            else
            {
                Debug.LogError("Could not load battery, player has no batteries");
            }
        }
    }

}