using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace Project.Runtime.Gameplay.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleMotor : MonoBehaviour
    {
        [Header("Acceleration")] public float horsePower;
        public AnimationCurve accelerationCurve;
        public float accelerationSpeed;
        public int maximumSpeed;
        public float currentSpeed;
        public bool ParkingBrake;

        public DriveType driveType = DriveType.BWD;

        public enum DriveType
        {
            BWD,
            FWD,
            AWD
        }

        [Space(5)] [Header("Braking")] public float brakePower;
        public float autoBrakePower;

        [Space(5)] [Header("Gear Switching")] public int currentGear;
        public float currentGearSpeed;

        [Header("Car Weight")] public Vector3 centerOfMass = new Vector3(0, -0.9f, 0);

        #region Internal Variables

        //Speed
        private float _currentAcceleration;
        private float _maxSpeedPerGear;
        internal float CurrentVelocity;

        //Braking
        internal bool IsBraking;
        internal bool ShouldAutoBrake;
        private float _currentBrakeForce;
        private float _currentAutoBraking;
        private float _currentParkingBrake;
        private float _parkingBrakePower = 100000000;
        [SerializeField] private float _currentTotalBrakePower;

        //Components
        private VehicleController _controller;
        private Rigidbody _rigidbody;

        #endregion

        public void SetValues()
        {
            //Get Components
            _controller = GetComponent<VehicleController>();
            _rigidbody = GetComponent<Rigidbody>();

            //Assign Variables
            _rigidbody.centerOfMass = centerOfMass;
            
        }

        private void FixedUpdate()
        {
            //Get current velocity of the rigidbody
            CurrentVelocity = _rigidbody.velocity.magnitude;
            currentSpeed = CurrentVelocity;
        }

        public void HandleMotor(float verticalInput)
        {
            //Receive input from VehicleController and calculate current ccceleration amount
            if (verticalInput > 0)
            {
                _currentAcceleration = verticalInput * horsePower * accelerationCurve.Evaluate((accelerationSpeed));
            }
            else if (verticalInput < 0)
            {
                _currentAcceleration = verticalInput * -horsePower * accelerationCurve.Evaluate((accelerationSpeed));
            }
            

            //Switch acceleration and clamp depending on currentGear
            switch (currentGear)
            {
                case 0:
                {
                    currentGearSpeed = _currentAcceleration * 0.10f;
                    _maxSpeedPerGear = 5;
                    break;
                }

                case 1:
                {
                    currentGearSpeed = _currentAcceleration * 0.25f;
                    _maxSpeedPerGear = 10;
                    break;
                }

                case 2:
                {
                    currentGearSpeed = _currentAcceleration * 0.50f;
                    _maxSpeedPerGear = 20;
                    break;
                }

                case 3:
                {
                    currentGearSpeed = _currentAcceleration;
                    _maxSpeedPerGear = 25;
                    break;
                }

                case 4:
                {
                    currentGearSpeed = _currentAcceleration;
                    _maxSpeedPerGear = maximumSpeed;
                    break;
                }

            }

            //Determine brakePower, autoBrakePower, and parkingBrake
            _currentBrakeForce = IsBraking ? brakePower : 0f;
            _currentAutoBraking = ShouldAutoBrake ? autoBrakePower : 0f;
            _currentParkingBrake = ParkingBrake ? _parkingBrakePower : 0f;

            //Switch drive type
            switch (driveType)
            {
                //Apply forces to wheels in VehicleController
                case DriveType.BWD:
                {
                    _controller.backLeft.motorTorque = verticalInput * currentGearSpeed;
                    _controller.backRight.motorTorque = verticalInput * currentGearSpeed;
                    break;
                }

                case DriveType.FWD:
                {
                    _controller.frontLeft.motorTorque = verticalInput * currentGearSpeed;
                    _controller.frontRight.motorTorque = verticalInput * currentGearSpeed;
                    break;
                }

                case DriveType.AWD:
                {
                    _controller.frontLeft.motorTorque = verticalInput * currentGearSpeed;
                    _controller.frontRight.motorTorque = verticalInput * currentGearSpeed;
                    _controller.backLeft.motorTorque = verticalInput * currentGearSpeed;
                    _controller.backRight.motorTorque = verticalInput * currentGearSpeed;
                    break;
                }
            }

            ApplyBraking();
        }

        private void ApplyBraking()
        {
            _currentTotalBrakePower = _currentBrakeForce + _currentAutoBraking + _currentParkingBrake;
            //Apply brakePower + autoBrakingPower to the wheels depending on context
            _controller.frontLeft.brakeTorque = _currentTotalBrakePower;
            _controller.frontRight.brakeTorque = _currentTotalBrakePower;
            _controller.backLeft.brakeTorque = _currentTotalBrakePower;
            _controller.backRight.brakeTorque = _currentTotalBrakePower;

            //Clamp speed to max
            if (_rigidbody.velocity.magnitude > maximumSpeed)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maximumSpeed);
            }
        }

    }
}
