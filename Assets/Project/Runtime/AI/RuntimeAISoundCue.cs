using System;
using System.Collections;
using UnityEngine;

namespace Project.Runtime.AI
{
    [RequireComponent(typeof(AudioSource))]
    public class RuntimeAISoundCue : MonoBehaviour
    {
        public AudioSource _source;
        private float _clipLength;
    }
}