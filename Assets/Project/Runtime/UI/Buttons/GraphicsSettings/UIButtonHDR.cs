using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonHDR : MonoBehaviour
    {
        public UniversalRenderPipelineAsset asset;


        public void OnEnable()
        {
            asset.supportsHDR = true;
        }

        public void OnDisable()
        {
            asset.supportsHDR = false;
        }
    }
}