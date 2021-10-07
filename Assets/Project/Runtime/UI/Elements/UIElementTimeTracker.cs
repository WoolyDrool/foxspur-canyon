using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIElementTimeTracker : MonoBehaviour
    {
        [Header("UI Elements")] public TextMeshProUGUI DayCounter;

        public TextMeshProUGUI MonthCounter;
        public TextMeshProUGUI TotalDayCounter;

        void Start()
        {

        }

        void Update()
        {
            DayCounter.text = GameManager.instance.timeManager.day.ToString();
            MonthCounter.text = GameManager.instance.timeManager.dayOfTheMonth.ToString() + " " +
                                GameManager.instance.timeManager.month.ToString();
            TotalDayCounter.text = "Day " + GameManager.instance.timeManager.currentDay.ToString();
        }
    }
}
