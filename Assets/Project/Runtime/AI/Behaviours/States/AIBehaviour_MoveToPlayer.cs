using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI.States
{
    public class AIBehaviour_MoveToPlayer : AIBaseBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            if(behaviourSet.pathingManager.path)
            {
                behaviourSet.pathingManager.EnableRepathing(true);
                Debug.Log("Did not have path to player, resolving");
            }

            behaviourSet.pathingManager.StopOnPath(false);
            behaviourSet.pathingManager.LockAgent(false);
            //behaviourSet.pathingManager.lookAtPlayer = true;
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
