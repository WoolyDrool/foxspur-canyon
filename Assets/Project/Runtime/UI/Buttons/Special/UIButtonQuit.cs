using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonQuit : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }

}