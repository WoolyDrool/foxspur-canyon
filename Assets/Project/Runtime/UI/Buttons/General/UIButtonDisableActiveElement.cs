using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonDisableActiveElement : MonoBehaviour
    {
        public void OnClick()
        {
            gameObject.SetActive(false);
        }
    }

}