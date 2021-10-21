
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIButtonGSPPDL : MonoBehaviour
{
    public UniversalRenderPipelineAsset urpAsset;
    public TextMeshProUGUI text;
    public void UpValue()
    {
        urpAsset.maxAdditionalLightsCount++;
        text.text = urpAsset.maxAdditionalLightsCount.ToString();
    }

    public void DownValue()
    {
        urpAsset.maxAdditionalLightsCount--;
        text.text = urpAsset.maxAdditionalLightsCount.ToString();
    }
}