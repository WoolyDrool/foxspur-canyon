using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Project.Runtime.UI.Elements
{
    public class UIElementFPSCounter : MonoBehaviour
    {
        public float refreshRate = 0.3f;
        
        private TextMeshProUGUI counter;
        private int avgFrameRate;
        private float timer;
        private float frameCount;
        private float dt;
        private float fps;
        private int displayfps;
        
        void Start()
        {
            counter = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            frameCount++;
            dt += Time.deltaTime;

            if (dt > 1.0 / refreshRate)
            {
                fps = frameCount / dt;
                frameCount = 0;
                dt -= 1.0f / refreshRate;
                displayfps = (int)fps;
            }
           
            counter.text = "fps:" + displayfps.ToString();
            timer = Time.unscaledTime + refreshRate;

            if (fps > 60)
            {
                counter.color = Color.green;
            }

            if (fps < 59)
            {
                counter.color = Color.yellow;
            }

            if (fps < 30)
            {
                counter.color = Color.red;
            }
        }
    }
}
