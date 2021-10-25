using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsHeadlamp : MonoBehaviour
    {
        public GameObject lightContainer;
        public Light lightSource;
        public AudioSource soundSource;
        public AudioSource hum;
        public AudioClip onSound;
        public AudioClip reloadSound;
        public AudioClip offSound;
        public bool headlampOn;
        
        [Header("Reloading")]
        public float reloadHoldTime;
        public float currentHoldTime;
        public float currentReloadTime;
        public GameObject reloadProgressGroup;
        public Image reloadProgressDial;
        
        [Header("Light Attentuation")]
        public Transform camera;
        public float tooCloseDistance;
        public float wayTooCloseDistance;
        public float tooCloseAttenuation;
        public float wayTooCloseAttentuation;
        public LayerMask collisionMask;
        public bool outOfBattery = false;
        public float attentuationLerpTime = 3;
        
        #region Internal Variables

        private InteractableBatteryDevice _batteryDevice;
        private PlayerInput _input;
        private bool reloading = false;
        private RaycastHit _hit;
        private float startingIntensity;
        private float currentIntensity;
        private float adjustedIntensity;
        private float lerpedIntensity;
        private float dimIntensity;
        [SerializeField] private float _initHoldTime = 0;
        
        private bool canReload = true;

        #endregion
        

        // Start is called before the first frame update
        void Start()
        {
            _batteryDevice = GetComponent<InteractableBatteryDevice>();
            _input = GetComponentInParent<PlayerInput>();
            startingIntensity = lightSource.intensity;
            dimIntensity = startingIntensity / 2;
        }

        // Update is called once per frame
        void Update()
        {
            currentIntensity = lightSource.intensity;
            LightAttenuation();
            DetermineBattery();

            if (!outOfBattery && !reloading)
            {
                if (_input.flashLightToggle)
                {
                    if (!headlampOn)
                    {
                        TurnOn();
                    }
                    else if(headlampOn)
                    {
                        TurnOff();
                    }
                }
            }

            if (outOfBattery)
            {
                if (headlampOn)
                {
                    headlampOn = false;
                    TurnOff();
                }
                canReload = true;
            }

            if (_input.flashLightHoldReload && !headlampOn)
            {
                if (canReload)
                {
                    _initHoldTime += 1 * Time.deltaTime;
                    
                    if (_initHoldTime > 0.5f)
                    {
                        if (GameManager.instance.playerInventory.currentBatteries >= 1)
                        {
                            Reload();
                        }
                        else
                        {
                            UIAlertUpdate.alert.AddAlertMessage(AlertType.GENERAL, "No batteries!");
                            return;
                        }
                    }
                }
            }
            else
            {
                _initHoldTime = 0;
                currentHoldTime = 0;
                reloadProgressGroup.SetActive(false);
            }
        }

        void Reload()
        {
            reloading = true;
            reloadProgressGroup.SetActive(true);

            currentHoldTime += 1 * Time.deltaTime;
            
            float reloadTime = currentHoldTime / reloadHoldTime;

            reloadProgressDial.fillAmount = reloadTime;

            if (currentHoldTime >= reloadHoldTime)
            {
                canReload = false;
                RenitDevice();
            }
        }

        void RenitDevice()
        {
            
            reloadProgressGroup.SetActive(false);
            currentHoldTime = 0;
            reloading = false;
            GameManager.instance.playerInventory.RemoveBattery();
            soundSource.PlayOneShot(reloadSound);
            _batteryDevice.AddBattery();
            StartCoroutine(ReloadCooldown());
            TurnOn();
        }

        void LightAttenuation()
        {
            if (Physics.Raycast(camera.position, camera.transform.forward, out _hit, tooCloseDistance, collisionMask))
            {
                float currentDistance = Vector3.Distance(transform.position, _hit.point);

                if (currentDistance <= tooCloseDistance)
                {
                    adjustedIntensity = startingIntensity - (currentDistance * -1) + -tooCloseAttenuation;
                    lerpedIntensity = Mathf.Lerp(currentIntensity, adjustedIntensity, attentuationLerpTime * Time.deltaTime);
                    lightSource.intensity = lerpedIntensity;

                    if (currentDistance <= wayTooCloseDistance)
                    {
                        adjustedIntensity = startingIntensity - (currentDistance * -1) + -wayTooCloseAttentuation;
                        lerpedIntensity = 0;
                        lerpedIntensity = Mathf.Lerp(currentIntensity, adjustedIntensity, attentuationLerpTime * Time.deltaTime);
                        lightSource.intensity = lerpedIntensity;
                    }
                }
            }
            else
            {
                if (lightSource.intensity < startingIntensity)
                {
                    lightSource.intensity = Mathf.Lerp(currentIntensity, startingIntensity, attentuationLerpTime * Time.deltaTime);
                }
            }
        }

        void DetermineBattery()
        {
            if (_batteryDevice.currentBatteryCharge > 1)
            {
                outOfBattery = false;
            }
            else if (_batteryDevice.currentBatteryCharge <= 0)
            {
                outOfBattery = true;
            }
        }

        void TurnOn()
        {
            Debug.Log("On");
            soundSource.PlayOneShot(onSound);
            hum.Play();
            lightContainer.SetActive(true);
            _batteryDevice.PowerOnDevice();
            headlampOn = true;
        }

        void TurnOff()
        {
            Debug.Log("Off");
            soundSource.PlayOneShot(offSound);
            hum.Stop();
            lightContainer.SetActive(false);
            _batteryDevice.PowerDownDevice();
            headlampOn = false;
        }
        
        IEnumerator ReloadCooldown()
        {
            reloading = false;
            yield return new WaitForSeconds(2);
            canReload = true;
        }
        
    }

}