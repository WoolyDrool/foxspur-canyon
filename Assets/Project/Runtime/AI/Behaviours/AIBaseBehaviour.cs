using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.AI.States;

namespace Project.Runtime.AI
{
    public class AIBaseBehaviour : StateMachineBehaviour
    {
        internal AIBaseBehaviourSet behaviourSet;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            behaviourSet = animator.gameObject.GetComponent<AIBaseBehaviourSet>();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
