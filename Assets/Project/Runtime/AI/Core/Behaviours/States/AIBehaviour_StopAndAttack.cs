using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI.States
{
    public class AIBehaviour_StopAndAttack : AIBaseBehaviour
    {
        public float attackLength;
        [SerializeField] float countDown;
        public float attackDamage = 15;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 
            base.OnStateEnter(animator, stateInfo, layerIndex);
            behaviourSet.combatManager.Attack(attackDamage);
            Debug.Log("Attacked at " + behaviourSet.currentAIStep.ToString());
            //behaviourSet.pathingManager.lookAtPlayer = true;
            if(countDown < 0.01f)
            {
                countDown = attackLength;
                Debug.LogWarning("Debug: Cooldown timer was less than 0, resetting...", animator.transform);
            }
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            countDown -= Time.deltaTime;
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            if (countDown <= 0)
            {
                base.OnStateExit(animator, stateInfo, layerIndex);
                behaviourSet.stateMachine.ChangeState(behaviourSet.stateMachine.previousState);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            countDown = attackLength;
        }

    }
}
