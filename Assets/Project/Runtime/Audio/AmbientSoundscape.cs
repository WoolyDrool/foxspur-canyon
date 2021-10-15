using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Runtime.Audio
{
    public class AmbientSoundscape : MonoBehaviour
    {
        public TimeManager time;

        public AudioClip[] daytimeMusicTracks;
        public AudioClip[] nightTimeMusicTracks;

        private bool _hasPlayedMusicToday = false;
        
        private void Awake()
        {
            time = GameManager.instance.timeManager;
        }

        void Start()
        {
            time.morningEvent.AddListener(HandleDaySounds);
            time.eveningEvent.AddListener(HandleNightSounds);
            
            time.nightEvent.AddListener(HandleNightMusic);
            time.noonEvent.AddListener(HandleDayMusic);
            time.midnightEvent.AddListener(ResetMusic);

            if (time.time == TimeManager.TimeOfDay.Night)
            {
                HandleNightSounds();
            }
            else
            {
                HandleDaySounds();
            }
        }

        private void ResetMusic()
        {
            _hasPlayedMusicToday = false;
        }

        private void HandleNightMusic()
        {
            if (!_hasPlayedMusicToday)
            {
                int chance = Random.Range(1, 10);
                if (chance == 5)
                {
                    int index = Random.Range(0, nightTimeMusicTracks.Length);
                    GameManager.instance.audioManager.PlayMusicTrack(nightTimeMusicTracks[index]);
                    _hasPlayedMusicToday = true;
                }
            }
            
        }

        private void HandleDayMusic()
        {
            if (!_hasPlayedMusicToday)
            {
                int chance = Random.Range(1, 10);
                if (chance == 5)
                {
                    int index = Random.Range(0, daytimeMusicTracks.Length);
                    GameManager.instance.audioManager.PlayMusicTrack(daytimeMusicTracks[index]);
                    _hasPlayedMusicToday = true;
                }
            }
        }

        private void HandleNightSounds()
        {
            Debug.Log("Playing nighttime sounds");
            GameManager.instance.audioManager.CrossfadeAmbientGroup1(0.005f, true);
        }

        private void HandleDaySounds()
        {
            GameManager.instance.audioManager.CrossfadeAmbientGroup1(0.01f, false);
        }
    }
}
