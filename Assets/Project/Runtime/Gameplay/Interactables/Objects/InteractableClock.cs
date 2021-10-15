using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableClock : MonoBehaviour
    {
        public TextMeshProUGUI hour;

        public TextMeshProUGUI minute;

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.timeManager.currentHour < 10)
            {
                hour.text = "0" + GameManager.instance.timeManager.currentHour.ToString();
            }
            else
            {
                hour.text = GameManager.instance.timeManager.currentHour.ToString();
            }

            if (GameManager.instance.timeManager.currentMinute < 10)
            {
                minute.text = "0" + GameManager.instance.timeManager.currentMinute.ToString();
            }
            else
            {
                minute.text = GameManager.instance.timeManager.currentMinute.ToString();
            }
        }
    }
}
