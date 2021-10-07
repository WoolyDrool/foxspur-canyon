using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ArmsManager : MonoBehaviour
    {
        public Animator arms;

        [SerializeField] private bool _isWatchRaised = false;
        [SerializeField] private bool _isBinocularRaised = false;
        [SerializeField] private bool _isHatchedEquipped = false;

        public ToolsBinoculars toolsBinoculars;
        public ToolsWatch toolsWatch;
        public ToolsHatchet toolsHatchet;

        private void Awake()
        {
            //_binoculars = GetComponent<Binoculars>();
        }

        // Update is called once per frame
        void Update()
        {
            

            if (_isHatchedEquipped)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    arms.Play("anim_player_hatchetSwing");
                }
            }
            
            if (!_isHatchedEquipped)
            {
                if (Input.GetKey(KeyCode.T) && !_isBinocularRaised)
                {
                    toolsWatch.RaiseWatch();
                    _isWatchRaised = true;
                    arms.SetBool("watchRaised", true);


                }
                else if (Input.GetKeyUp(KeyCode.T))
                {
                    toolsWatch.LowerWatch();
                    _isWatchRaised = false;
                    arms.SetBool("watchRaised", false);
                }

                if (Input.GetKey(KeyCode.B) && !_isWatchRaised)
                {
                    _isBinocularRaised = true;
                    toolsBinoculars.ZoomIn();
                    arms.SetBool("binocularsRaised", true);
                }
                else if (Input.GetKeyUp(KeyCode.B))
                {
                    _isBinocularRaised = false;
                    toolsBinoculars.ZoomOut();
                    toolsBinoculars.Test();
                    arms.SetBool("binocularsRaised", false);
                }
            }
        }

        public void RaiseBinoculars()
        {
            toolsBinoculars.ZoomIn();
        }
    }

}