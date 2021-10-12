using System;
using System.Collections.Generic;
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

        [Header("Events")] 
        public Dictionary<string, Action<Dictionary<string, object>>> aiEvents;

        #region Internal Variables

        internal RuntimeAIAudioManager _audioManager;
        internal RuntimeAIPlayerTracker _playerTracker;

        private static RuntimeAIManager _aiManager;

        #endregion

        public static RuntimeAIManager instance
        {
            get
            {
                if (!_aiManager)
                {
                    _aiManager = FindObjectOfType(typeof(RuntimeAIManager)) as RuntimeAIManager;

                    if (!_aiManager)
                    {
                        Debug.LogError("No AI Manager!");
                    }
                    else
                    {
                        _aiManager.Init();
                    }
                }

                return _aiManager;
            }
        }

        #region Events
        private void Init()
        {
            if (aiEvents == null)
            {
                aiEvents = new Dictionary<string, Action<Dictionary<string, object>>>();
            }
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (instance.aiEvents.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.aiEvents[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.aiEvents.Add(eventName, thisEvent);
            }
        }
        
        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (_aiManager == null) return;
            Action<Dictionary<string, object>> thisEvent;
            if (instance.aiEvents.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                instance.aiEvents[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
            Action<Dictionary<string, object>> thisEvent = null;
            if (instance.aiEvents.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
        #endregion

        private void Awake()
        {
            _audioManager = GetComponent<RuntimeAIAudioManager>();
            _playerTracker = GetComponent<RuntimeAIPlayerTracker>();
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