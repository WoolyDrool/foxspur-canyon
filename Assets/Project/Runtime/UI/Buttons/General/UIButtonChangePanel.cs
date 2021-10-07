using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonChangePanel : MonoBehaviour
    {
        public GameObject currentPanel;
        public GameObject nextPanel;

        void Start()
        {

        }

        void Update()
        {

        }

        public void OnClick()
        {
            nextPanel.SetActive(true);
            currentPanel.SetActive(false);
        }
    }
}
