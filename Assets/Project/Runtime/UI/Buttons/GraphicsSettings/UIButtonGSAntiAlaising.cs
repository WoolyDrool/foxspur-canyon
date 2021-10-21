using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonGSAntiAlaising : MonoBehaviour
    {
        private UniversalRenderPipelineAsset _pipeline;
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
