using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonInteractionSound : MonoBehaviour
    {
        public AudioClip hoverSound;
        public AudioClip clickSound;
        public AudioClip dragSound;

        private AudioSource source;

        void Start()
        {
            source = GetComponentInParent<AudioSource>();
        }

        public void PlayHoverSound()
        {
            source.clip = hoverSound;
            if (!source.isPlaying)
                source.Play();
        }

        public void PlayClickSound()
        {
            source.clip = clickSound;
            if (!source.isPlaying)
                source.Play();
        }

        public void PlayClickAndDragSound()
        {
            source.clip = dragSound;

            if (!source.isPlaying)
                source.Play();
        }
    }
}
