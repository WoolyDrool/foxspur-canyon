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
        
        public bool headlampOn;
        public bool outOfBattery = false;
        
        [Header("Sound")]
        public AudioSource mainSource;
        public AudioSource humSource;
        public AudioClip onSound;
        public AudioClip reloadSound;
        public AudioClip offSound;
        
        [Header("Reloading")]
        public float reloadHoldTime;
        public float currentHoldTime;
        public float currentReloadTime;
        public GameObject reloadProgressGroup;
        public Image reloadProgressDial;
        
        [Header("Light Attentuation")]
        public Transform sceneCamera;
        public float attenuationLerpTime = 3;
        public float tooCloseDistance = 5;
        public float wayTooCloseDistance = 3;
        public float tooCloseAttenuation = 0.3f;
        public float wayTooCloseAttenuation = 0.4f;
        public LayerMask atnRayColMask;
        
        #region Internal Variables

        private InteractableBatteryDevice _batteryDevice;
        private PlayerInputManager _inputManager;
        private bool _reloading = false;
        private RaycastHit _hit;
        private float _startingIntensity;
        private float _currentIntensity;
        private float _adjustedIntensity;
        private float _lerpedIntensity;
        private float _initHoldTime = 0;
        private bool _canReload = true;

        #endregion
        

        // Start is called before the first frame update
        void Start()
        {
            _batteryDevice = GetComponent<InteractableBatteryDevice>();
            _inputManager = GetComponentInParent<PlayerInputManager>();
            _startingIntensity = lightSource.intensity;
        }

        // Update is called once per frame
        void Update()
        {
            _currentIntensity = lightSource.intensity;
            LightAttenuation();
            DetermineBattery();

            if (!outOfBattery && !_reloading)
            {
                if (_inputManager.flashLightToggle)
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
                _canReload = true;
            }

            if (_inputManager.flashLightHoldReload && !headlampOn)
            {
                if (_canReload)
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
            _reloading = true;
            reloadProgressGroup.SetActive(true);

            currentHoldTime += 1 * Time.deltaTime;
            
            float reloadTime = currentHoldTime / reloadHoldTime;

            reloadProgressDial.fillAmount = reloadTime;

            if (currentHoldTime >= reloadHoldTime)
            {
                _canReload = false;
                RenitDevice();
            }
        }

        void RenitDevice()
        {
            
            reloadProgressGroup.SetActive(false);
            currentHoldTime = 0;
            _reloading = false;
            GameManager.instance.playerInventory.RemoveBattery();
            mainSource.PlayOneShot(reloadSound);
            _batteryDevice.AddBattery();
            StartCoroutine(ReloadCooldown());
            TurnOn();
        }

        void LightAttenuation()
        {
            if (Physics.Raycast(sceneCamera.position, sceneCamera.transform.forward, out _hit, tooCloseDistance, atnRayColMask))
            {
                float currentDistance = Vector3.Distance(transform.position, _hit.point);

                if (currentDistance <= tooCloseDistance)
                {
                    _adjustedIntensity = _startingIntensity - (currentDistance * -1) + -tooCloseAttenuation;
                    _lerpedIntensity = Mathf.Lerp(_currentIntensity, _adjustedIntensity, attenuationLerpTime * Time.deltaTime);
                    lightSource.intensity = _lerpedIntensity;

                    if (currentDistance <= wayTooCloseDistance)
                    {
                        _adjustedIntensity = _startingIntensity - (currentDistance * -1) + -wayTooCloseAttenuation;
                        _lerpedIntensity = 0;
                        _lerpedIntensity = Mathf.Lerp(_currentIntensity, _adjustedIntensity, attenuationLerpTime * Time.deltaTime);
                        lightSource.intensity = _lerpedIntensity;
                    }
                }
                else
                {
                    if (lightSource.intensity < _startingIntensity)
                    {
                        lightSource.intensity = Mathf.Lerp(_currentIntensity, _startingIntensity, attenuationLerpTime * Time.deltaTime);
                    }
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
            mainSource.PlayOneShot(onSound);
            humSource.Play();
            lightContainer.SetActive(true);
            _batteryDevice.PowerOnDevice();
            headlampOn = true;
        }

        void TurnOff()
        {
            Debug.Log("Off");
            mainSource.PlayOneShot(offSound);
            humSource.Stop();
            lightContainer.SetActive(false);
            _batteryDevice.PowerDownDevice();
            headlampOn = false;
        }
        
        IEnumerator ReloadCooldown()
        {
            _reloading = false;
            yield return new WaitForSeconds(2);
            _canReload = true;
        }
        
    }

}