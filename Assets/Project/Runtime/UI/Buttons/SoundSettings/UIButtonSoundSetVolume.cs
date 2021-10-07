using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonSoundSetVolume : MonoBehaviour
    {
        public string valueName;
        public AudioMixer mixerGroup;

        public void SetLevel(float sliderValue)
        {
            mixerGroup.SetFloat(valueName, Mathf.Log10(sliderValue) * 20);
        }
    }
}
