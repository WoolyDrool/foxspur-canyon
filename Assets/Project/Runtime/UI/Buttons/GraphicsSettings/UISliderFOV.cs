using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderFOV : MonoBehaviour
{
    public float minFOV;
    public float maxFOV;
    public float currentFOV;
    private Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
        currentFOV = GameManager.instance.settingsManager.playerPrefFOV;
        _slider.minValue = minFOV;
        _slider.maxValue = maxFOV;
        _slider.value = (currentFOV);
    }

    void Update()
    {
        
    }

    public void AdjustFOV(float newValue)
    {
        currentFOV = newValue;
        GameManager.instance.cameraManager.cameraComponent.fieldOfView = currentFOV;
        GameManager.instance.settingsManager.playerPrefFOV = currentFOV;
    }
    
}
