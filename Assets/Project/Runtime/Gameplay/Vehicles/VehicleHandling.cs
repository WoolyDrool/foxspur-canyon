using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Vehicles
{
    public class VehicleHandling : MonoBehaviour
    {
        private VehicleController _controller;

        [Header("Steering")] public AnimationCurve steeringCurve;
        public float steeringSpeed;
        public float steeringAngle;

        public SteeringType steeringType = SteeringType.FWS;

        public enum SteeringType
        {
            FWS,
            RWS,
            AWS
        }

        #region Internal Variables

        private float _currentSteeringAmount;
        private float _currentSteeringAngle;
        private float _input;

        #endregion

        public void SetValues()
        {
            //Get components
            _controller = GetComponent<VehicleController>();
        }

        public void HandleSteering(float horizontalInput)
        {
            //Receive input from VehicleController and calculate current steering amount
            _input = horizontalInput;
            _currentSteeringAmount = _input * (steeringAngle * steeringCurve.Evaluate(steeringSpeed));
            _currentSteeringAngle = _currentSteeringAmount * steeringAngle;

            switch (steeringType)
            {
                case SteeringType.FWS:
                {
                    FrontWheelSteering();
                    break;
                }
                case SteeringType.RWS:
                {
                    RearWheelSteering();
                    break;
                }
                case SteeringType.AWS:
                {
                    AllWheelSteering();
                    break;
                }
            }
        }

        #region Steering Types

        private void FrontWheelSteering()
        {
            if (_input != 0)
            {
                _controller.frontLeft.steerAngle = _currentSteeringAngle;
                _controller.frontRight.steerAngle = _currentSteeringAngle;
            }
            else
            {
                _controller.frontLeft.steerAngle = 0;
                _controller.frontRight.steerAngle = 0;
            }
        }

        private void RearWheelSteering()
        {
            if (_input != 0)
            {
                _controller.backLeft.steerAngle = _currentSteeringAngle;
                _controller.backRight.steerAngle = _currentSteeringAngle;
            }
            else
            {
                _controller.backLeft.steerAngle = 0;
                _controller.backRight.steerAngle = 0;
            }
        }

        private void AllWheelSteering()
        {
            if (_input != 0)
            {
                _controller.frontLeft.steerAngle = _currentSteeringAngle;
                _controller.frontRight.steerAngle = _currentSteeringAngle;
                _controller.backLeft.steerAngle = _currentSteeringAngle;
                _controller.backRight.steerAngle = _currentSteeringAngle;
            }
            else
            {
                _controller.frontLeft.steerAngle = 0;
                _controller.frontRight.steerAngle = 0;
                _controller.backLeft.steerAngle = 0;
                _controller.backRight.steerAngle = 0;
            }
        }

        #endregion

    }
}
