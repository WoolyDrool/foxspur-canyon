using Project.Runtime.UI.HUD;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIElementTrailProgress : MonoBehaviour
    {
        public TextMeshProUGUI percentageText;
        private string completionPercentageN;
        public GameObject mainProgress;

        public void Toggle()
        {
            mainProgress.SetActive(!mainProgress.activeSelf);
        }
        
        public void UpdateTrailProgressUI(float completionPercentage)
        {
            completionPercentageN = 0 + (completionPercentage * 100).ToString("F1");
            percentageText.text = completionPercentageN + "/100.0"; 
        }

        public void FinishTrailProgressUI()
        {
            percentageText.text = "100/100.0"; 
            percentageText.color = Color.green;
            GameManager.instance.hudManager.ChangePlayerHudState(PlayerHudState.TRAIL_COMPLETE);
        }
    }
}