using System;
using UnityEngine;

namespace Project.Runtime.AI
{
    public class RuntimeAIPlayerTracker : MonoBehaviour
    {
        private RuntimeAIManager _manager;
        public Transform playerTransform;
        public Vector3 currentPlayerPosition;

        private void Awake()
        {
            _manager = GetComponent<RuntimeAIManager>();
        }

        public void Update()
        {
            if (_manager.aggressionState != AggressionLevel.DISABLED)
            {
                currentPlayerPosition = playerTransform.position;
            }
        }
    }
}