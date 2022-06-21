using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.UI.Buttons
{
    public class UISliderMouseSensitivity : MonoBehaviour
    {
        public float minSensitivity;
        public float maxSensitivity;
        public float currentSensitivity;
        private Slider _slider;
        
        void Start()
        {
            _slider = GetComponent<Slider>();
            currentSensitivity = GameManager.instance.settingsManager.playerPrefMouseSensitivity;
            _slider.minValue = minSensitivity;
            _slider.maxValue = maxSensitivity;
            _slider.value = (currentSensitivity / 1);
        }
        
        public void AdjustSensitivity(float newValue)
        {
            currentSensitivity = GameManager.instance.inputManager.gamepad ? newValue : newValue * 1.5f;
            
            GameManager.instance.settingsManager.playerPrefMouseSensitivity = currentSensitivity;
        }
    }
}