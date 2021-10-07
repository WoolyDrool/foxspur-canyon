using System;
using UnityEngine;

namespace Project.Runtime.AI
{
    public enum AggressionLevel
    {
        DISABLED,
        CALM,
        UPSET,
        FURIOUS,
        HYPERAGGRESSION
    }
    [RequireComponent(typeof(RuntimeAIAudioManager))]
    [RequireComponent(typeof(RuntimeAIPlayerTracker))]
    public class RuntimeAIManager : MonoBehaviour
    {
        public AggressionLevel aggressionState;
        public int currentAggression = 0;

        [Header("Controls")] 
        public int calmThreshold;
        public int upsetThreshold;
        public int furiousThreshold;
        public int hyperThreshold;

        #region Internal Variables

        internal RuntimeAIAudioManager _audioManager;
        internal RuntimeAIPlayerTracker _playerTracker;

        #endregion

        private void Awake()
        {
            _audioManager = GetComponent<RuntimeAIAudioManager>();
            _playerTracker = GetComponent<RuntimeAIPlayerTracker>();
        }

        public void AddAggression(int amount)
        {
            currentAggression += amount;
            UpdateAggressionLevel();
        }

        public void RemoveAggression(int amount)
        {
            currentAggression -= amount;
            UpdateAggressionLevel();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _audioManager.SpawnAudioNearPlayer(40, 60);
            }
        }

        public void UpdateAggressionLevel()
        {
            if (currentAggression >= calmThreshold)
            {
                aggressionState = AggressionLevel.CALM;
            }

            if (currentAggression >= upsetThreshold)
            {
                aggressionState = AggressionLevel.UPSET;
            }

            if (currentAggression >= furiousThreshold)
            {
                aggressionState = AggressionLevel.FURIOUS;
            }

            if (currentAggression >= hyperThreshold)
            {
                aggressionState = AggressionLevel.HYPERAGGRESSION;
            }
        }
    }
}