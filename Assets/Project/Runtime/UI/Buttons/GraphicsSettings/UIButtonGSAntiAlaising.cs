using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonGSAntiAlaising : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        private void OnEnable()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        }

        private void OnDisable()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
    }
}
