using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    public class RuntimeProfileInterpreter : MonoBehaviour
    {
        public PlayerProfile runtimeProfile;

        private void Awake()
        {
            try
            {
                runtimeProfile = SerializationManager.Load();
            }
            catch (Exception e)
            {
                InjectProfile(runtimeProfile);
                Console.WriteLine(e);
                throw;
            }
        }

        void Start()
        {
            
        }

        void Update()
        {

        }

        public void InjectProfile(PlayerProfile profile)
        {
            runtimeProfile = profile;
            //Debug.Log("Received profile " + profile.playerName);
        }
    }

}