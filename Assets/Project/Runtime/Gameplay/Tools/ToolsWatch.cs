using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsWatch : MonoBehaviour
    {
        public delegate void ShowPlayerUI();

        public static event ShowPlayerUI OnRaise;

        private bool _isRaised;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKey(KeyCode.T) && !_isRaised)
            {
                RaiseWatch();
            }
            else if (Input.GetKeyUp(KeyCode.T) && _isRaised)
            {
                LowerWatch();
            }
        }

        public void RaiseWatch()
        {
            if (OnRaise != null && !_isRaised)
                OnRaise();
            _isRaised = true;
        }

        public void LowerWatch()
        {
            if (OnRaise != null && _isRaised)
                OnRaise();
            _isRaised = false;
        }
    }
}
