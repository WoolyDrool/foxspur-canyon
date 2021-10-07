using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI
{
    [RequireComponent(typeof(Animator))]
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField]
        internal string currentState;
        public string defaultState;
        internal string previousState;
        internal string nullState = "ANY STATE";
        internal Animator machine;
        internal bool machineEnabled = false;

        private void Awake()
        {
            machine = GetComponent<Animator>();
            //currentState = nullState;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //machineEnabled = machine.enabled ? false : true;
        }

        public void ChangeState(string nextState)
        {
            if (nextState == currentState)
            {
                return;
            }
            previousState = currentState;
            currentState = nextState;
            machine.Play(currentState);
            //Debug.LogWarning("Changed state from " + previousState + " to " + nextState + " at " + GetComponent<AIBaseBehaviourSet>().currentAIStep.ToString());
        }

        public void ShuffleState(string[] states)
        {
            int index = Random.Range(0, states.Length);

            string randomState = states[index];

            ChangeState(randomState);
        }

        public void DisableStateMachine()
        {
            Debug.LogWarning("Disabled stated machine at " + GetComponent<AIBaseBehaviourSet>().currentAIStep.ToString());
            ChangeState(nullState);
            machine.enabled = false;
        }

        public void EnableStateMachine()
        {
            Debug.LogWarning("Enabled stated machine at " + GetComponent<AIBaseBehaviourSet>().currentAIStep.ToString());
            machine.enabled = true;
            ChangeState(defaultState);
        }
    }
}
