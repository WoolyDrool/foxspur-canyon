using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Runtime.AI.States
{
    public class AIBehaviour_Wander : AIBaseBehaviour
    {
        public float minTickRange = 1;
        public float maxTickRange = 5;
        public float walkRadius = 10;

        float tickRate;
        float currentTick;

        Vector3 nextPoint;
        Vector3 homePosition;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(behaviourSet.pathingManager.path)
                behaviourSet.pathingManager.DisableRepathing();
            homePosition = animator.transform.position;
            currentTick = minTickRange;
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            currentTick -= Time.deltaTime;

            if(currentTick <= 0)
            {
                SelectRandomPoint(animator);

                tickRate = Random.Range(minTickRange, maxTickRange);

                currentTick = tickRate;
            }
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        void SelectRandomPoint(Animator animator)
        {
            Vector3 randomPoint = Random.insideUnitSphere * walkRadius;

            randomPoint += homePosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;

            behaviourSet.pathingManager.SetNewDestination(finalPosition);
            Debug.Log("Picked a new point of " + finalPosition.ToString() + " at " + behaviourSet.currentAIStep.ToString());
        }
    }

}

