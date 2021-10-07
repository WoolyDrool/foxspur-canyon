using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsRadio : EquippableObject
    {
        private const string CLICK_TRIGGER = "Click";

        public AudioClip clickSound;
        private AudioSource _source;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _source.PlayOneShot(clickSound);
                objectAnimator.SetTrigger(CLICK_TRIGGER);
            }
        }

        public void ClickRadio()
        {
            objectAnimator.SetTrigger(CLICK_TRIGGER);
        }
    }

}