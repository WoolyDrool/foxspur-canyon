using System;
using Project.Runtime.Global;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Project.Runtime.Serialization;
using Random = UnityEngine.Random;

namespace Project.Runtime.Gameplay.Player
{
    public enum MortalityState
    {
        HEALTHY,
        WOUNDED,
        DEAD
    }
    public class VitalHealthFunctionality : MonoBehaviour
    {
        public MortalityState currentMortalityState;
        public AudioSource source;
        public AudioClip deathSting;
        public AudioMixerGroup mixerGroup;

        public Animator playerAnimator;
        public Image deathOverlay;
        public GameObject tools;
        private PlayerProfileInterpreter _interpreter;
        private BaseVital _health;
        private PlayerVitals _vitals;
        private AudioClip[] _damageSounds;
        private const string DEATH_TRIGGER = "Death";

        private void OnEnable()
        {
            
        }

        public void Start()
        {
            _interpreter = FindObjectOfType<PlayerProfileInterpreter>();
            _damageSounds = _interpreter.gender.damageSounds;
            _vitals = GetComponent<PlayerVitals>();
            _health = _vitals.healthStat;
        }

        private void Update()
        {
     
        }

        public void ChangeState(MortalityState state)
        {
            if(currentMortalityState == state)
                return;

            currentMortalityState = state;

            switch (currentMortalityState)
            {
                case MortalityState.HEALTHY:
                {
                    break;
                }
                case MortalityState.WOUNDED:
                {
                    break;
                }
                case MortalityState.DEAD:
                {
                    PerformDeathFunctions();
                    break;
                }
            }
        }

        private void PerformDeathFunctions()
        {
            GameManager.instance.cameraManager.canLook = false;
            tools.SetActive(false);
            playerAnimator.SetTrigger(DEATH_TRIGGER);
            deathOverlay.gameObject.SetActive(true);
            EventManager.TriggerEvent("ShowDeathMenu", null);
            EventManager.TriggerEvent("ToggleMouse", null);
            GameManager.instance.audioManager.PlaySoundOnce(deathSting, mixerGroup);
        }

        public void TakeDamage(float damageToTake)
        {
            _health.RemoveValue(damageToTake);
            if (damageToTake > _health.currentValue)
            {
                ChangeState(MortalityState.DEAD);
                source.gameObject.AddComponent<AudioReverbFilter>().reverbPreset = AudioReverbPreset.Psychotic;
                source.PlayOneShot(_interpreter.gender.deathSound);
            }
            else
            {
                PlayDamageSound();
            }
            
        }

        public void PlayDamageSound()
        {
            int n = Random.Range(1, _damageSounds.Length);
            source.clip = _damageSounds[n];
            source.PlayOneShot(source.clip);
        }
    }
}