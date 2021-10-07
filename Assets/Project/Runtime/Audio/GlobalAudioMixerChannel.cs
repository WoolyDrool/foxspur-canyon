using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Audio
{
    public class GlobalAudioMixerChannel : MonoBehaviour
    {
        public GlobalAudioMixer masterMixer;

        private AudioSource localSource;

        public float currentClipLength;

        private AudioLowPassFilter lowFilter;
        private AudioHighPassFilter hiFilter;
        private AudioReverbFilter revFilter;
        private void Awake()
        {
            localSource = GetComponent<AudioSource>();
        }

        public void WakeUp(bool isLoop)
        {
            if (!isLoop)
            {
                currentClipLength = localSource.clip.length;

                StartCoroutine(WaitForClip(currentClipLength));
            }
        }

        /// <summary>
        /// control1 = cutoff frequency, control2 = Q factor
        /// </summary>
        /// <param name="control1"></param>
        /// <param name="control2"></param>

        public void AddLowPassFilter(float control1, float control2)
        {
            lowFilter = gameObject.AddComponent<AudioLowPassFilter>();
            lowFilter.cutoffFrequency = control1;
            lowFilter.lowpassResonanceQ = control2;
        }
        
        /// <summary>
        /// control1 = cutoff frequency, control2 = Q factor
        /// </summary>
        /// <param name="control1"></param>
        /// <param name="control2"></param>
        public void AddHiPassFilter(float control1, float control2)
        {
            hiFilter = gameObject.AddComponent<AudioHighPassFilter>();
            hiFilter.cutoffFrequency = control1;
            hiFilter.highpassResonanceQ = control2;
        }
        
        /// <summary>
        /// Preset index: 0 (Off), 1 (Alley), 2 (Cave), 3 (Forest), 4 (Sewer)
        /// </summary>
        /// <param name="preset"></param>
        /// <param name="control1"></param>
        /// <param name="control2"></param>
        public void AddReverbFilter(int preset)
        {
            revFilter = gameObject.AddComponent<AudioReverbFilter>();
            switch (preset)
            {
                case 0:
                {
                    revFilter.reverbPreset = AudioReverbPreset.Off;
                    break;
                }
                case 1:
                {
                    revFilter.reverbPreset = AudioReverbPreset.Alley;
                    break;
                }
                
                case 2:
                {
                    revFilter.reverbPreset = AudioReverbPreset.Cave;
                    break;
                }
                
                case 3:
                {
                    revFilter.reverbPreset = AudioReverbPreset.Forest;
                    break;
                }
                
                case 4:
                {
                    revFilter.reverbPreset = AudioReverbPreset.SewerPipe;
                    break;
                }
            }
        }

        void RemoveAllEffects()
        {
            if(revFilter)
                Destroy(revFilter);
            if(hiFilter)
                Destroy(hiFilter);
            if(lowFilter)
                Destroy(lowFilter);
        }
        
        public void Sleep()
        {
            localSource.clip = null;
            masterMixer.availableMiscChannels++;
            RemoveAllEffects();
            gameObject.SetActive(false);
        }

        IEnumerator WaitForClip(float duration)
        {
            yield return new WaitForSeconds(duration);
            Sleep();
            StopAllCoroutines();
        }
    }
}
