using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

        public Transform camera;
        
        public bool headlampOn;

        public float reloadHoldTime;
        public float currentHoldTime;
        public float currentReloadTime;

        public float tooCloseDistance;
        public float wayTooCloseDistance;
        public float tooCloseAttenuation;
        public float wayTooCloseAttentuation;
        private RaycastHit _hit;
        public LayerMask collisionMask;

        #region Internal Variables

        private InteractableBatteryDevice _batteryDevice;
        private PlayerInput _input;
        private bool reloading = false;
        
        private float startingIntensity;
        private float currentIntensity;
        private float adjustedIntensity;
        private float lerpedIntensity;
        private float dimIntensity;
        public float attentuationLerpTime = 3;
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
            currentIntensity = actualLight.intensity;
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


            if (Physics.Raycast(camera.position, camera.transform.forward, out _hit, tooCloseDistance, collisionMask))
            {
                float currentDistance = Vector3.Distance(transform.position, _hit.point);

                if (currentDistance <= tooCloseDistance)
                {
                    adjustedIntensity = startingIntensity - (currentDistance * -1) + -tooCloseAttenuation;
                    lerpedIntensity = Mathf.Lerp(currentIntensity, adjustedIntensity, attentuationLerpTime * Time.deltaTime);
                    actualLight.intensity = lerpedIntensity;

                    if (currentDistance <= wayTooCloseDistance)
                    {
                        adjustedIntensity = startingIntensity - (currentDistance * -1) + -wayTooCloseAttentuation;
                        lerpedIntensity = 0;
                        lerpedIntensity = Mathf.Lerp(currentIntensity, adjustedIntensity, attentuationLerpTime * Time.deltaTime);
                        actualLight.intensity = lerpedIntensity;
                    }
                }
            }
            else
            {
                if (actualLight.intensity < startingIntensity)
                {
                    actualLight.intensity = Mathf.Lerp(currentIntensity, startingIntensity, attentuationLerpTime * Time.deltaTime);
                }
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