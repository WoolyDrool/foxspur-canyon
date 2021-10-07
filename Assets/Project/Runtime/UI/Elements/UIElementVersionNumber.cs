using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIElementVersionNumber : MonoBehaviour
    {
        public GameData data;
        private TextMeshProUGUI text;

        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();

            if (data.state != BuildState.DEMO)
            {
                text.text = data.state.ToString() + data.majorVersion.ToString() + "." + data.majorVersion.ToString() + "." +
                            data.patch.ToString() + "." +
                            data.buildVersion.ToString();
            }
            else
            {
                text.text = BuildState.DEMO.ToString();
            }
            
        }

        void Update()
        {

        }
    }

}