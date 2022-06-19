using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.Gameplay.Tools;
using Project.Runtime.Gameplay.Vehicles;
using Project.Runtime.Global;
using Project.Runtime.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.UI.HUD
{
    public enum PlayerHudState
    {
        PAUSE,
        INV,
        MAP,
        QUESTS,
        NOTES,
        TRAIL_COMPLETE,
        NONE
    }
    public class UIElementsHudManager : MonoBehaviour
    {
        private PlayerVitals _playerVitals;
        public AudioSource _uiSound;
        [Header("Elements - Main")] public RectTransform dialsGroup;
        public RectTransform timeGroup;
        public GameObject pauseMenu;
        public GameObject drivingHud;
        public GameObject batteryHud;
        public GameObject inventoryHud;
        public GameObject journalHud;
        public GameObject payoutHud;
        public GameObject currentHud;
        public UIElementTrailProgress trailProgressHud;

        public GameObject overallPlayerHud;

        public PlayerHudState curHudState;
        public GameObject[] hudObj;
        public GameObject prevHud;
        
        [Header("Elements - Specific")] 
        public Image healthDial;
        public Image hungerDial;
        public Image hydrationDial;
        public Image sleepDial;
        public Image batterySegments;
        
        [Header("Elements - Overlays")] public GameObject binocularsOverlay;
        public GameObject speedometerDial;
        public GameObject gearShift;
        public GameObject parkingBrake;

        [Header("Elements - Sound")] public AudioClip inventorySound;

        #region Internal Variables

        private DrivingMovement _drivingMovement;
        private const float maxSpeedAngle = -190;
        private const float zeroSpeedAngle = 0;
        private float shiftAmount;
        public bool showingPlayerHud = false;
        private int currentHudIndex = 0;

        #endregion

        private void OnEnable()
        {
            ToolsBinoculars.OnRaise += ShowBinocularHud;
            VehicleInteractions.OnEnter += ShowDrivingHud;
            ToolsWatch.OnRaise += ShowPlayerHud;
            InteractableBatteryDevice.OnToggle += ShowBatteryIndicator;
        }
        
        private void OnDisable()
        {
            ToolsBinoculars.OnRaise -= ShowBinocularHud;
            VehicleInteractions.OnEnter -= ShowDrivingHud;
            ToolsWatch.OnRaise -= ShowPlayerHud;
            InteractableBatteryDevice.OnToggle -= ShowBatteryIndicator;
        }

        void Start()
        {
            _drivingMovement = GameManager.instance.playerManager.playerTransform.GetComponent<DrivingMovement>();
            _playerVitals = GameManager.instance.playerVitals;
            ChangePlayerHudState(PlayerHudState.NONE);
        }

        void Update()
        {
            #region UI Updates
            
            UpdatePlayerHud();
            
            if (drivingHud.activeSelf)
            {
                UpdateDrivingHud();
            }

            if (batteryHud.activeSelf)
            {
                UpdateBatteryIndicator();
            }

            if (curHudState == PlayerHudState.NONE)
            {
                overallPlayerHud.SetActive(true);
            }
            else
            {
                overallPlayerHud.SetActive(false);
            }
            #endregion
        }

        public void ChangePlayerHudState(PlayerHudState state)
        {
            if (state == PlayerHudState.NONE)
            {
                EventManager.TriggerEvent("ReturnToNormal", null);
            }
            else
            {
                EventManager.TriggerEvent("FreeMouse", null);
                EventManager.TriggerEvent("FreeLook", null);
            }
            
            if (curHudState == state)
            {
                ChangePlayerHudState(PlayerHudState.NONE);
                return;
            }

            switch (state)
            {
                case PlayerHudState.PAUSE:
                    hudObj[0].SetActive(true);
                    currentHud = hudObj[0];
                    currentHudIndex = 0;
                    break;
                case PlayerHudState.INV:
                    hudObj[1].SetActive(true);
                    currentHud = hudObj[1];
                    currentHudIndex = 1;
                    break;
                case PlayerHudState.MAP:
                    hudObj[2].SetActive(true);
                    currentHud = hudObj[2];
                    currentHudIndex = 2;
                    break;
                case PlayerHudState.QUESTS:
                    hudObj[3].SetActive(true);
                    currentHud = hudObj[3];
                    currentHudIndex = 3;
                    break;
                case PlayerHudState.NOTES:
                    hudObj[4].SetActive(true);
                    currentHud = hudObj[4];
                    currentHudIndex = 4;
                    break;
                case PlayerHudState.TRAIL_COMPLETE:
                    hudObj[5].SetActive(true);
                    currentHud = hudObj[5];
                    currentHudIndex = 5;
                    break;
                case PlayerHudState.NONE:
                    foreach (GameObject g in hudObj)
                    {
                        g.SetActive(false);
                    }
                    currentHud = null;
                    break;
            }

            curHudState = state;
        }

        private void ShowPlayerHud()
        {
            if (!showingPlayerHud)
            {
                showingPlayerHud = true;
                //dialsTweener.easeType = LeanTweenType.easeInSine;
                //timeTweener.easeType = LeanTweenType.easeInSine;
                //UpdatePlayerHud();

            }
            else if (showingPlayerHud)
            {
                showingPlayerHud = false;
                //dialsTweener.easeType = LeanTweenType.easeOutSine;
                //timeTweener.easeType = LeanTweenType.easeOutSine;
            }

        }
        
        void UpdatePlayerHud()
        {
            //UpdateUI
            hungerDial.fillAmount = Normalize(_playerVitals.currentHunger, 100);
            hydrationDial.fillAmount = Normalize(_playerVitals.currentHydration, 100);
            sleepDial.fillAmount = Normalize(_playerVitals.currentSleep, 100);
            healthDial.fillAmount = Normalize(_playerVitals.currentHealth, 100);
            
            float Normalize(float a, float b)
            {
                return a / b;
            }
        }

        void ShowBinocularHud()
        {
            Debug.Log("Called");
            if (!binocularsOverlay.activeSelf)
            {
                binocularsOverlay.SetActive(true);
            }
            else
            {
                binocularsOverlay.SetActive(false);
            }
        }

        

        void ShowDrivingHud()
        {
            if (!drivingHud.activeSelf)
            {
                drivingHud.SetActive(true);
            }
            else
            {
                drivingHud.SetActive(false);
            }
        }

        void UpdateDrivingHud()
        {
            float rotationAmount = _drivingMovement.currentVehicle.currentSpeed / 180;
            int currentSpeedAsInt = (int) _drivingMovement.currentVehicle.currentSpeed;

            switch (_drivingMovement.currentVehicle.currentGear)
            {
                case 0:
                {
                    shiftAmount = -180;
                    break;
                }
                case 1:
                {
                    shiftAmount = -90;
                    break;
                }
                case 2:
                {
                    shiftAmount = 0;
                    break;
                }
                case 3:
                {
                    shiftAmount = 90;
                    break;
                }
                case 4:
                {
                    shiftAmount = 180;
                    break;
                }
            }

            speedometerDial.transform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
            gearShift.transform.localPosition = new Vector3(0, shiftAmount, 0);
            parkingBrake.gameObject.SetActive(_drivingMovement.currentVehicle.ParkingBrake);
        }

        void ShowBatteryIndicator()
        {
            if (!batteryHud.activeSelf)
            {
                batteryHud.SetActive(true);
            }
            else
            {
                batteryHud.SetActive(false);
            }
        }

        void UpdateBatteryIndicator()
        {
            //UpdateUI
            batterySegments.fillAmount = Normalize(_playerVitals.currentToolBattery, 100);

            float Normalize(float a, float b)
            {
                return a / b;
            }
        }

        private float GetSpeedRotation()
        {
            float totalAngleSize = zeroSpeedAngle - maxSpeedAngle;
            float normalizedSpeed =
                _drivingMovement.currentVehicle.currentSpeed / _drivingMovement.currentVehicle.maximumSpeed;

            return zeroSpeedAngle - normalizedSpeed * totalAngleSize;
        }
        
    }
}
