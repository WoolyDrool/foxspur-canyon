using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public struct GlobalMixerGroups
{
    public static string AMBIENCE1 = "ambience1Volume";
    public static string AMBIENCE2 = "ambience2Volume";
    public static string AMBIENCE3 = "ambience3Volume";
    public static string AMBIENCE4 = "ambience4Volume";
}

namespace Project.Runtime.Audio
{
    public class GlobalAudioMixer : MonoBehaviour
    {
        [Header("Main Channels")] public AudioSource[] ambience;
        [Space(5)] public AudioSource music;
        public AudioSource[] userInterface;
        public AudioSource player;

        [Header("Other")] public AudioSource[] miscChannels = new AudioSource[8];
        public int availableMiscChannels;
        public AudioSource nextMiscSource;
        public AudioClip debugClip;
        public AudioMixerGroup debugGroup;
        public AudioMixer debugMixer;

        #region Internal Variables

        private AudioSource _nextAmbientSource;
        private AudioSource _nextInterfaceSource;

        #endregion

        void Start()
        {
            //PlaySoundOnce(debugClip, debugGroup);
        }

        #region Ambient Sounds

        AudioSource FindAvailableAmbientChannel()
        {
            for (int i = 0; i < ambience.Length; i++)
            {
                if (!ambience[i].isPlaying)
                {
                    _nextAmbientSource = ambience[i];
                    //availableChannels--;
                }
            }

            return nextMiscSource;
        }

        void SelectAvailableAmbientChannel()
        {
            FindAvailableAmbientChannel();
            _nextAmbientSource.gameObject.SetActive(true);
        }

        public void PlayAmbientSound(AudioClip clipToPlay)
        {
            Debug.Log("Called");
            SelectAvailableAmbientChannel();
            _nextAmbientSource.clip = clipToPlay;
            _nextAmbientSource.GetComponent<GlobalAudioMixerChannel>().WakeUp(true);
            _nextAmbientSource.Play();
        }

        public void CrossfadeAmbientGroup1(float crossfadeTime, bool direction)
        {
            Crossfade utility = new Crossfade(GlobalMixerGroups.AMBIENCE1, GlobalMixerGroups.AMBIENCE4, crossfadeTime, debugMixer, direction);
            utility.isCrossfading = true;
            StartCoroutine(utility.CrossFade());
        }
        
        public void CrossfadeAmbientGroup2(float crossfadeTime, bool direction)
        {
            Crossfade utility = new Crossfade(GlobalMixerGroups.AMBIENCE2, GlobalMixerGroups.AMBIENCE3, crossfadeTime, debugMixer, direction);
            utility.isCrossfading = true;
            StartCoroutine(utility.CrossFade());
        }

        #endregion

        #region Interface Sounds

        AudioSource FindAvailableInterfaceChannel()
        {
            for (int i = 0; i < userInterface.Length; i++)
            {
                if (!userInterface[i].isPlaying)
                {
                    _nextInterfaceSource = userInterface[i];
                    //availableChannels--;
                }
            }

            return _nextInterfaceSource;
        }

        void SelectAvailableInterfaceChannel()
        {
            FindAvailableInterfaceChannel();
            _nextInterfaceSource.gameObject.SetActive(true);
        }

        public void PlayInterfaceSound(AudioClip clipToPlay)
        {
            SelectAvailableInterfaceChannel();
            _nextInterfaceSource.clip = clipToPlay;
            _nextInterfaceSource.GetComponent<GlobalAudioMixerChannel>().WakeUp(false);
            _nextInterfaceSource.Play();
        }

        #endregion

        #region General Sounds

        void UpdateAvailableMiscChannels()
        {
            for (int i = 0; i < miscChannels.Length; i++)
            {
                if (!miscChannels[i].isPlaying)
                {
                    availableMiscChannels++;
                }
            }
        }

        AudioSource FindAvailableMiscChannel()
        {
            for (int i = 0; i < miscChannels.Length; i++)
            {
                if (!miscChannels[i].isPlaying)
                {
                    nextMiscSource = miscChannels[i];
                    //availableChannels--;
                }
            }

            return nextMiscSource;
        }

        void SelectAvailableMiscChannel()
        {
            FindAvailableMiscChannel();
            UpdateAvailableMiscChannels();
            availableMiscChannels--;
            nextMiscSource.gameObject.SetActive(true);
        }

        public void PlaySoundOnce(AudioClip clipToPlay, AudioMixerGroup mixer)
        {
            SelectAvailableMiscChannel();
            nextMiscSource.clip = clipToPlay;
            nextMiscSource.outputAudioMixerGroup = mixer;
            nextMiscSource.GetComponent<GlobalAudioMixerChannel>().WakeUp(false);
            nextMiscSource.Play();
        }

        #endregion

        #region Music

        public void PlayMusicTrack(AudioClip clipToPlay)
        {
            music.gameObject.SetActive(true);
            music.clip = clipToPlay;
            music.GetComponent<GlobalAudioMixerChannel>().WakeUp(false);
            music.Play();
        }

        #endregion

        #region World Sounds

        public void SpawnAudioNearPlayer(Vector3 pos, float minDistance, float maxDistance, GameObject objectToSpawn)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            Vector3 knownPosition = pos;

            knownPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            //knownPosition.x = Mathf.Clamp(knownPosition.x, minDistance, maxDistance);
            knownPosition.y = knownPosition.y;
            // knownPosition.z = Mathf.Clamp(knownPosition.z, minDistance, maxDistance);

            GameObject clone;

            clone = Instantiate(objectToSpawn.gameObject, knownPosition, transform.rotation);
        }

        #endregion
    }
}
