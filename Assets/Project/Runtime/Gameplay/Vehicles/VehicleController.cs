using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Vehicles
{
    [RequireComponent(typeof(VehicleMotor))]
    [RequireComponent(typeof(VehicleHandling))]
    [RequireComponent(typeof(VehicleSound))]
    [RequireComponent(typeof(VehicleInteractions))]
    public class VehicleController : MonoBehaviour
    {
        [Header("Control")] public bool playerInCar = false;
        public WheelCollider frontLeft, frontRight, backLeft, backRight;

        #region Internal Variables

        //Variables
        private float _verticalInput;
        private float _horizontalInput;

        //Components
        [HideInInspector] public VehicleMotor _motor;
        [HideInInspector] public VehicleHandling _handling;
        [HideInInspector] public VehicleSound _sound;
        [HideInInspector] public VehicleInteractions _interactions;

        #endregion

        private void Start()
        {
            //Get required components
            _motor = GetComponent<VehicleMotor>();
            _handling = GetComponent<VehicleHandling>();
            _sound = GetComponent<VehicleSound>();
            _interactions = GetComponent<VehicleInteractions>();

            //Set required component values
            _motor.SetValues();
            _handling.SetValues();
            _sound.SetValues();
            _interactions.SetValues();
            
        }

        private void Update()
        {
            if (playerInCar)
                GetInput();
        }

        private void GetInput()
        {
            //Get input from GameManager
            _verticalInput = GameManager.instance.playerManager.PlayerInputManager.input.y;
            _horizontalInput = GameManager.instance.playerManager.PlayerInputManager.input.x;
            _motor.ShouldAutoBrake = GameManager.instance.playerManager.PlayerInputManager.input.y == 0;

            #region Interactions

            //Gear Up
            if (Input.GetButtonDown("Sprint"))
            {
                if (_motor.currentGear < 4)
                {
                    _motor.currentGear++;
                }
                else
                {
                    _motor.currentGear = 0;
                }
            }

            //Gear Down
            if (Input.GetButtonDown("Crouch"))
            {
                if (_motor.currentGear > 0)
                {
                    _motor.currentGear--;
                }
                else
                {
                    _motor.currentGear = 4;
                }
            }

            //Headlights
            if (Input.GetButtonDown("Headlights"))
            {
                _interactions.ToggleHeadlights();
            }

            //Interior Lights
            if (Input.GetButtonDown("Interior Lights"))
            {
                _interactions.ToggleInteriorLights();

            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!_motor.ParkingBrake)
                {
                    _motor.ParkingBrake = true;
                }
                else
                {
                    _motor.ParkingBrake = false;
                }
            }

            //Braking
            _motor.IsBraking = Input.GetButton("Jump");

            //Braking Sound
            if (Input.GetButtonDown("Jump"))
            {
                if (_motor.CurrentVelocity > _motor.maximumSpeed / 2)
                {
                    _sound.HandleBrakingSound(true);
                }
                else
                {
                    _sound.HandleBrakingSound(false);
                    return;
                }
            }

            //Exit Car
            if (Input.GetButtonDown("Interact"))
            {
                if (_interactions.CanExit)
                {
                    _interactions.ExitVehicle();
                }
                else
                {
                    return;
                }
            }

            #endregion
        }

        private void FixedUpdate()
        {
            if (playerInCar)
            {
                _motor.HandleMotor(_verticalInput);
                _handling.HandleSteering(_horizontalInput);
                _sound.HandleEngineSound(_motor.CurrentVelocity, _motor.maximumSpeed);
            }

        }

    }
}
