using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Runtime.Audio
{
    public class Crossfade
    {
        public AudioMixer mainMixer;
        public string track1Name;
        public string track2Name;
        public float crossfadeDuration;

        public bool isCrossfading = false;
        private float t;
        private bool inOrOut = true;

        private float crossfade1;
        private float crossfade2;

        public Crossfade(string _track1, string _track2, float duration, AudioMixer mixer, bool direction)
        {
            track1Name = _track1;
            track2Name = _track2;
            crossfadeDuration = duration;
            mainMixer = mixer;
        }

        public IEnumerator CrossFade()
        {
            Debug.Log("Called");
            while (isCrossfading)
            {
                t += crossfadeDuration * Time.deltaTime;
                
                //IMPLEMENTATION:
                // True = Turn down track 1 down and bring up track 2
                // False = Bring up track 1 and turn down track 2
                if (inOrOut == true)
                {
                    crossfade1 = Mathf.Lerp(0, -80, t);
                    crossfade2 = Mathf.Lerp(-80, 0, t);
                }
                else if(inOrOut == false)
                {
                    crossfade1 = Mathf.Lerp(-80, 0, t);
                    crossfade2 = Mathf.Lerp(0, -80, t);
                }
                

                mainMixer.SetFloat(track1Name, crossfade1);
                mainMixer.SetFloat(track2Name, crossfade2);

                yield return null;
            }

            isCrossfading = false;
        }
    }

}