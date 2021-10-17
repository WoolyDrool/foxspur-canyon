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
        public AudioClip reloadSound;
        public AudioClip offSound;
        public GameObject reloadProgressGroup;
        public Image reloadProgressDial;

        public bool headlampOn;

        public float reloadHoldTime;
        public float currentHoldTime;
        public float currentReloadTime;

        #region Internal Variables

        private InteractableBatteryDevice _batteryDevice;
        private PlayerInput _input;
        private bool reloading = false;
        
        private float startingIntensity;
        private float dimIntensity;
        private bool canReload = true;

        #endregion
        

        // Start is called before the first frame update
        void Start()
        {
            _batteryDevice = GetComponent<InteractableBatteryDevice>();
            _input = GetComponentInParent<PlayerInput>();
            startingIntensity = actualLight.intensity;
            dimIntensity = startingIntensity / 2;
        }

        // Update is called once per frame
        void Update()
        {
            if (_input.flashLightToggle && currentHoldTime < 0.5f)
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

            if (_input.flashLightHoldReload && !headlampOn)
            {
                if (canReload)
                {
                    currentHoldTime += 1 * Time.deltaTime;
                    
                    if(!reloadProgressGroup.activeSelf)
                        reloadProgressGroup.SetActive(true);
                    
                    reloadProgressDial.fillAmount = currentHoldTime / reloadHoldTime;
                    
                    if (currentHoldTime >= reloadHoldTime)
                    {
                        LoadBattery();
                        return;
                    }
                }
            }
            else
            {
                reloadProgressGroup.SetActive(false);
                currentHoldTime = 0;
            }
        }

        IEnumerator ReloadCooldown()
        {
            reloading = false;
            yield return new WaitForSeconds(2);
            canReload = true;
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
                currentHoldTime = 0;
                reloading = true;
                canReload = false;
                Debug.Log("Loaded battery");
                actualLight.intensity = startingIntensity;
                GameManager.instance.playerInventory.RemoveBattery();
                _batteryDevice.AddBattery();
                soundSource.PlayOneShot(reloadSound);
                StartCoroutine(ReloadCooldown());
            }
            else
            {
                Debug.LogError("Could not load battery, player has no batteries");
            }
        }
    }

}