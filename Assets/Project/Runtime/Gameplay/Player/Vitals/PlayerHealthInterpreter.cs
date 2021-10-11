using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Player;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player
{
    public class PlayerHealthInterpreter : MonoBehaviour
    {
        private VitalHealthFunctionality _playerVitals;

        void Start()
        {
            _playerVitals = FindObjectOfType<VitalHealthFunctionality>();
        }

        void Update()
        {

        }

        public void TakeDamage(float damage)
        {
            _playerVitals.TakeDamage(damage);
        }
    }

}