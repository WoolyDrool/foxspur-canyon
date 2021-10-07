using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Vehicles
{
    [RequireComponent(typeof(Interactable))]
    public class VehicleInteractions : MonoBehaviour
    {
        [Header("Vehicle Info")] public string vehicleName = "Default Vehicle";

        [Header("Interactions")] public Transform seatPosition;
        public Quaternion seatRotation;
        public Transform exitPosition;
        public Quaternion exitRotation;

        internal bool PlayerHasEnteredOnce = false;

        [Header("Lights")] public GameObject headlights;
        public GameObject interiorLights;

        [Header("Particle Effects")] public ParticleSystem exhaustParticles;
        public ParticleSystem drivingParticles;

        public delegate void ShowVehicleHud();
        public static event ShowVehicleHud OnEnter;

        #region Internal Variables

        //Variables
        private Vector3 _seatPositionVec;
        private Vector3 _exitPositionVec;

        //Components
        private VehicleController _controller;
        private Interactable _interactable;

        //Control
        private float _defaultInteractRange;
        internal bool CanExit;
        private DrivingMovement _drivingMovement;

        #endregion

        public void SetValues()
        {
            //Get components
            _controller = GetComponent<VehicleController>();
            _interactable = GetComponent<Interactable>();
            _drivingMovement = GameManager.instance.playerManager.playerTransform.GetComponent<DrivingMovement>();

            //Set variables
            SetNewPositions();
            
            //Set interaction name
            _interactable.description = "Drive " + vehicleName;
            _defaultInteractRange = _interactable.interactRange;
            
            //Turn headlights/interior lights off
            headlights.SetActive(false);
            interiorLights.SetActive(false);
            
        }

        #region Interactions

        public void EnterVehicle()
        {
            //Tell the vehicle the player has entered, and that they cannot exit
            _controller.playerInCar = true;
            PlayerHasEnteredOnce = true;
            CanExit = false;

            //Disable the interact component to get rid of the UI
            _interactable.interactRange = 0;
            
            //Adjust parent and position
            SetNewPositions();
            GameManager.instance.playerManager.playerTransform.parent = this.transform;
            GameManager.instance.playerManager.playerTransform.position = _seatPositionVec;
            
            //Update the players movement status
            GameManager.instance.playerManager._playerController.ChangeStatus(Status.driving);
            _drivingMovement.UpdateCurrentVehicle();

            //Disable player
            GameManager.instance.playerManager._playerMovement.enabled = false;
            GameManager.instance.playerManager._playerCharacterController.enabled = false;

            //Start exit countdown
            StartCoroutine(WaitToExit());
            
            //Play sounds
            _controller._sound.HandleInteractionSound(true);
            
            if (OnEnter != null)
                OnEnter();
            
        }

        public void ExitVehicle()
        {
            //Tell the vehicle the player has exited
            _controller.playerInCar = false;
            _controller._motor.ParkingBrake = true;

            //Enable the interact component
            _interactable.interactRange = _defaultInteractRange;

            //Update the players movement status
            GameManager.instance.playerManager._playerController.ChangeStatus(Status.idle);
            _drivingMovement.UpdateCurrentVehicle();

            //Adjust parent and position
            SetNewPositions();
            GameManager.instance.playerManager.playerTransform.parent = null;
            GameManager.instance.playerManager.playerTransform.position = _exitPositionVec;

            //Enable player
            GameManager.instance.playerManager._playerMovement.enabled = true;
            GameManager.instance.playerManager._playerCharacterController.enabled = true;
            
            //Play sounds
            _controller._sound.HandleInteractionSound(false);
            if (OnEnter != null)
                OnEnter();
        }

        #endregion

        #region Lights

        public void ToggleHeadlights()
        {
            if (!headlights.activeSelf)
            {
                headlights.SetActive(true);
            }
            else
            {
                headlights.SetActive(false);
            }


        }

        public void ToggleInteriorLights()
        {
            if (!interiorLights.activeSelf)
            {
                interiorLights.SetActive(true);
            }
            else
            {
                interiorLights.SetActive(false);
            }
        }

        #endregion

        #region Particles

        //TODO: Particle systems go here...

        #endregion

        #region Calculations

        private void SetNewPositions()
        {
            _seatPositionVec = seatPosition.position;
            _exitPositionVec = exitPosition.position;
        }

        private IEnumerator WaitToExit()
        {
            yield return new WaitForSeconds(3);
            CanExit = true;
            yield break;
        }

        #endregion
    }
}
