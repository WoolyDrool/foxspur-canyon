using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    public class RuntimeProfileInterpreter : MonoBehaviour
    {
        public PlayerProfile runtimeProfile;
        void Start()
        {

        }

        void Update()
        {

        }

        public void InjectProfile(PlayerProfile profile)
        {
            runtimeProfile = profile;
            Debug.Log("Received profile " + profile.playerName);
        }
    }

}