using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsBinoculars : MonoBehaviour
    {
        private float startingFov;
        public float zoomFov;
        [SerializeField] private float currentZoomFov;

        public delegate void RaiseBinoculars();

        public static event RaiseBinoculars OnRaise;

        public Camera playerCamera;

        private bool _zoomed;

        void Start()
        {
            playerCamera = Camera.main;
            startingFov = playerCamera.fieldOfView;
            currentZoomFov = zoomFov;
        }

        void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && _zoomed)
            {
                if (currentZoomFov ! > zoomFov)
                {
                    currentZoomFov -= 10;

                    playerCamera.fieldOfView = currentZoomFov;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && _zoomed)
            {

                if (currentZoomFov ! < startingFov)
                {
                    currentZoomFov += 10;
                    playerCamera.fieldOfView = currentZoomFov;
                }
            }
        }

        public void ZoomIn()
        {
            if (OnRaise != null && !_zoomed)
                OnRaise();
            _zoomed = true;
            playerCamera.fieldOfView = currentZoomFov;

        }

        public void Test()
        {
            Debug.Log("test");
        }

        public void ZoomOut()
        {
            if (OnRaise != null && _zoomed)
                OnRaise();
            _zoomed = false;
            playerCamera.fieldOfView = startingFov;

        }
    }

}