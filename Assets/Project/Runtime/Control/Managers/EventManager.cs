using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Global
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

        private static EventManager _eventManager;

        public static EventManager instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!_eventManager)
                    {
                        Debug.LogError("No EventManager!");
                    }
                    else
                    {
                        _eventManager.Init();
                        
                        //DontDestroyOnLoad(_eventManager);
                    }
                }

                return _eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
            }
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }
        
        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener) {
            if (_eventManager == null) return;
            Action<Dictionary<string, object>> thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message) {
            Action<Dictionary<string, object>> thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
                thisEvent.Invoke(message);
            }
        }
    }
}