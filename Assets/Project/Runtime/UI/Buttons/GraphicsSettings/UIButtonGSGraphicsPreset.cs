using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonGSGraphicsPreset : MonoBehaviour
    {
        private string[] names;
        private TextMeshProUGUI qualityText;
        public int currentGraphicsSettingIndex;
        private int defaultGraphicsIndex = 5;

        private void Awake()
        {
            names = QualitySettings.names;
            qualityText = GetComponent<TextMeshProUGUI>();
            QualitySettings.SetQualityLevel(5, true);
            currentGraphicsSettingIndex = defaultGraphicsIndex;
            qualityText.text = names[defaultGraphicsIndex];
        }

        public void UpClick()
        {
            if (currentGraphicsSettingIndex < 5)
            {
                currentGraphicsSettingIndex++;
            }
            else
            {
                currentGraphicsSettingIndex = 0;
            }

            QualitySettings.SetQualityLevel(currentGraphicsSettingIndex, true);
            qualityText.text = names[currentGraphicsSettingIndex];
        }

        public void DownClick()
        {
            if (currentGraphicsSettingIndex > 0)
            {
                currentGraphicsSettingIndex--;
            }
            else
            {
                currentGraphicsSettingIndex = 5;
            }

            QualitySettings.SetQualityLevel(currentGraphicsSettingIndex, true);
            qualityText.text = names[currentGraphicsSettingIndex];
        }

    }
}
