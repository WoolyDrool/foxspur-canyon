using System;
using UnityEngine;
using Project.Runtime.Global;

namespace Project.Runtime.Environment
{
    public class EnvironmentalStreetlight : MonoBehaviour
    {
        public GameObject lightObj;
        public TimeManager.TimeOfDay enableTime;
        public TimeManager.TimeOfDay disableTime;

        private TimeManager _time;

        private void Start()
        {
            _time = GameManager.instance.timeManager;
        }

        private void Update()
        {
            if (_time)
            {
                if (_time.time == enableTime)
                {
                    lightObj.SetActive(true);
                }

                if (_time.time == disableTime)
                {
                    lightObj.SetActive(false);
                }
            }
        }
    }
}