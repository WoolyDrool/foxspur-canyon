using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Project.Runtime.Global;
using Project.Runtime.Gameplay.Vehicles;
using Project.Runtime.Gameplay.Tools;
using Project.Runtime.Gameplay.Player;


namespace Project.Runtime.UI.Elements
{
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
        public GameObject uiCantAddItem;
        public GameObject uiCantAddItemToTrash;
        public GameObject uiOutOfBags;
        public GameObject cantPick;

        [Header("Elements - Sound")] public AudioClip inventorySound;

        #region Internal Variables

        private DrivingMovement _drivingMovement;
        private const float maxSpeedAngle = -190;
        private const float zeroSpeedAngle = 0;
        private float shiftAmount;
        private Vector2 timeGroupStart;
        private Vector2 timeGroupEnd;
        private UITweener timeTweener;
        public bool showingPlayerHud = false;

        #endregion

        private void OnEnable()
        {
            ToolsBinoculars.OnRaise += ShowBinocularHud;
            VehicleInteractions.OnEnter += ShowDrivingHud;
            ToolsWatch.OnRaise += ShowPlayerHud;
            InteractableBatteryDevice.OnToggle += ShowBatteryIndicator;
            EventManager.StartListening("cantAddItem", OnCantAddItem);
            EventManager.StartListening("cantAddItemToTrash", OnCantAddItemToTrash);
            EventManager.StartListening("noBags", OnOutOfBags);
            EventManager.StartListening("cantPick", OnCantPick);
        }

        private void OnDisable()
        {
            ToolsBinoculars.OnRaise -= ShowBinocularHud;
            VehicleInteractions.OnEnter -= ShowDrivingHud;
            ToolsWatch.OnRaise -= ShowPlayerHud;
            InteractableBatteryDevice.OnToggle -= ShowBatteryIndicator;
            EventManager.StopListening("cantAddItem", OnCantAddItem);
            EventManager.StopListening("cantAddItemToTrash", OnCantAddItemToTrash);
            EventManager.StopListening("cantPick", OnCantPick);
            EventManager.StopListening("noBags", OnOutOfBags);
        }

        public void OnCantAddItem(Dictionary<string, object> message)
        {
            uiCantAddItem.SetActive(true);
        }
        
        public void OnCantAddItemToTrash(Dictionary<string, object> message)
        {
            uiCantAddItemToTrash.SetActive(true);
        }
        
        public void OnCantPick(Dictionary<string, object> message)
        {
            cantPick.SetActive(true);
        }
        
        public void OnOutOfBags(Dictionary<string, object> message)
        {
            uiOutOfBags.SetActive(true);
        }

        void Start()
        {
            _drivingMovement = GameManager.instance.playerManager.playerTransform.GetComponent<DrivingMovement>();
            _playerVitals = GameManager.instance.playerVitals;
            timeTweener = timeGroup.GetComponent<UITweener>();
            timeGroupStart = timeTweener.@from;
            timeGroupEnd = timeTweener.to;
        }

        void Update()
        {
            if (drivingHud.activeSelf)
            {
                UpdateDrivingHud();
            }

            if (batteryHud.activeSelf)
            {
                UpdateBatteryIndicator();
            }
            
            UpdatePlayerHud();
        }

        private void ShowPlayerHud()
        {
            if (!showingPlayerHud)
            {
                showingPlayerHud = true;
                //dialsTweener.easeType = LeanTweenType.easeInSine;
                //timeTweener.easeType = LeanTweenType.easeInSine;
                
                timeTweener.to = timeGroupEnd;
                timeTweener.@from = timeGroupStart;
                
                timeTweener.Restart();
                //UpdatePlayerHud();

            }
            else if (showingPlayerHud)
            {
                showingPlayerHud = false;
                //dialsTweener.easeType = LeanTweenType.easeOutSine;
                //timeTweener.easeType = LeanTweenType.easeOutSine;
                timeTweener.@from = timeGroupEnd;
                timeTweener.to = timeGroupStart;
                
                timeTweener.Restart();
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

        void ShowPauseHud()
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
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

        void ShowInventory()
        {
            _uiSound.clip = inventorySound;
            _uiSound.Play();
            if (!inventoryHud.activeSelf)
            {
                inventoryHud.SetActive(true);
            }
            else
            {
                inventoryHud.SetActive(false);
            }
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
