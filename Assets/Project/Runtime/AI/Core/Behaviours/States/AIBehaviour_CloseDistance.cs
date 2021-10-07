using UnityEngine;

namespace Project.Runtime.AI.States
{
    public class AIBehaviour_CloseDistance : AIBaseBehaviour
    {
        public float desiredDistance;
        public float speedToGetThere;
        private float _oldSpeed;
        private float _currentDistance;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 
            base.OnStateEnter(animator, stateInfo, layerIndex);
            behaviourSet.pathingManager.ChangeCurrentSpeed(speedToGetThere);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _currentDistance = behaviourSet.pathingManager.distanceToPlayer;

            if (_currentDistance <= desiredDistance)
            {
                base.OnStateExit(animator, stateInfo, layerIndex);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            behaviourSet.pathingManager.ResetCurrentSpeed();
            canEnterState = false;
            behaviourSet.BeginStateCooldown(this);
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}