using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIElementStatusText : MonoBehaviour
{
    public string message = "null";
    public Color messageColor;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = message;
        text.color = messageColor;
        
        Destroy(this, 5);
    }

    void Update()
    {
        
    }
}
