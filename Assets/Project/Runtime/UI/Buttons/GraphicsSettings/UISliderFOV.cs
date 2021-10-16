using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderFOV : MonoBehaviour
{
    private Camera _camera;
    public float minFOV = 60;
    public float maxFOV = 124.4f;
    public float currentFOV;
    private Slider _slider;
    void Awake()
    {
        _camera = Camera.main;
        _slider = GetComponent<Slider>();

        currentFOV = _camera.fieldOfView;
        _slider.minValue = minFOV;
        _slider.maxValue = maxFOV;
        _slider.value = _camera.fieldOfView / maxFOV;
    }

    void Update()
    {
        
    }

    public void AdjustFOV(float newValue)
    {
        currentFOV = newValue;
        _camera.fieldOfView = currentFOV;
    }
    
}
