using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Runtime.Gameplay.Vehicles
{
    public class VehicleSound : MonoBehaviour
    {
        [Header("Clips")] [SerializeField] private AudioClip enterSound;
        [SerializeField] private AudioClip exitSound;
        [SerializeField] private AudioClip motorSound;
        [SerializeField] private AudioClip interiorSound;
        [SerializeField] private AudioClip skidSound;

        [Space(5)] [Header("Sources")] public AudioMixer mixerGroup;
        [SerializeField] private AudioSource carAudio, engineAudio, interiorAudio;

        #region Internal Variables

        //Components
        private VehicleController _controller;

        //Mixer
        private const string MixerGroupName = "VEHICLES";

        #endregion

        public void SetValues()
        {
            //Get components
            _controller = GetComponent<VehicleController>();

            //Set mixer groups
            carAudio.outputAudioMixerGroup = mixerGroup.FindMatchingGroups(MixerGroupName)[0];
            engineAudio.outputAudioMixerGroup = mixerGroup.FindMatchingGroups(MixerGroupName)[0];
            interiorAudio.outputAudioMixerGroup = mixerGroup.FindMatchingGroups(MixerGroupName)[0];
            
            //Assign static clips
            engineAudio.clip = motorSound;
            interiorAudio.clip = interiorSound;
        }

        public void HandleInteractionSound(bool state)
        {
            if (state)
            {
                carAudio.clip = enterSound;
                
                carAudio.Play();
                engineAudio.Play();
                interiorAudio.Play();
            }
            else
            {
                carAudio.clip = exitSound;
                
                carAudio.Play();
                engineAudio.Stop();
                interiorAudio.Stop();
            }

        }

        public void HandleEngineSound(float currentSpeed, float maximumSpeed)
        {
            //Dynamically change the pitch of the engine hum depending on speed
            //TODO: Turn this into an AnimationCurve
            if (currentSpeed < 5)
                engineAudio.pitch = -1;

            if (currentSpeed > 10)
                engineAudio.pitch = 0.5f;

            if (currentSpeed > 15)
                engineAudio.pitch = 1;

            if (currentSpeed <= maximumSpeed)
                engineAudio.pitch = 1.2f;
        }

        public void HandleBrakingSound(bool skid)
        {
            if (skid)
            {
                //Play skid sound when braking
                carAudio.clip = skidSound;
                carAudio.Play();
            }
            else
            {

            }
        }
    }
}
