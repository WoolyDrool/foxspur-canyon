using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Runtime.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIPathingManager : MonoBehaviour
    {
        public bool path = false;
        [Space(5)]
        
        [Header("Variables")]
        public float distanceToPlayer;
        public float distanceToTarget;
        public Vector3 currentTarget;

        #region Internal Variables

        internal AIAwareness _behaviourSet;
        internal NavMeshAgent agent;
        internal Vector3 currentPosition;
        internal Transform playerTransform;
        internal Vector3 playerPosition;
        
        private bool _trackingPlayer = false;

        #endregion

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        #region Intialization
        
        public void InitPathing()
        {
            playerTransform = _behaviourSet.playerManager.playerTransform;
        }
        
        public void EnableRepathing(bool pathToPlayer)
        {
            path = true;
            
            _behaviourSet.stateMachine.ChangeState(_behaviourSet.stateMachine.defaultState);
            
            if (pathToPlayer)
            {
                _trackingPlayer = true;
                Debug.Log("Enabled repathing at " + _behaviourSet.currentAIStep.ToString());
            }
            else
            {
                _trackingPlayer = false;
                return;
            }
        }

        public void DisableRepathing()
        {
            path = false;
            Debug.Log("Disabled repathing at " + _behaviourSet.currentAIStep.ToString());
        }

        #endregion

        public void PongUpdate()
        {
            // Usage:
            // Gets called at regular intervals by AIAwareness.
            // This is done in place of a regular Update function to avoid
            // slowing down the game with too many AI calls at runtime.

            if (playerTransform)
            {
                currentPosition = transform.position;
                playerPosition = playerTransform.position;

                distanceToPlayer = Vector3.Distance(currentPosition, playerPosition);

                distanceToTarget = Vector3.Distance(currentPosition, currentTarget);
            }
            

            if (agent.enabled)
            {
                if (path && !_trackingPlayer)
                { 
                    
                }
                else if (_trackingPlayer)
                {
                    currentTarget = playerPosition;
                    agent.SetDestination(currentTarget);
                }
            }
                     
        }
        
        #region Movement Functions

        public void LockAgent(bool locked)
        {
            if(!locked)
            {
                agent.enabled = true;
                agent.updatePosition = true;
                agent.updateRotation = true;
            }
            else
            {
                agent.enabled = false;
                agent.updatePosition = false;
                agent.updateRotation = false;
            }
        }
        
        public void SetNewDestination(Vector3 destination)
        {
            // Usage:
            // Interrupts the agents current path and moves it to Vector3(Destination)

            agent.SetDestination(destination);
        }

        public void StopOnPath(bool stop)
        {
            // Usage:
            // Stops the agent immediately
            Debug.Log("StopOnPath called at " + _behaviourSet.currentAIStep.ToString() + " with a value of " + stop.ToString());

            if(!stop)
            {
                path = true;
                agent.isStopped = false;
            }
            else
            {  
                path = false;
                agent.isStopped = true;
            }
            
        }

        public void LookAtPlayer()
        {
            Vector3 direction = (playerPosition - transform.position).normalized;
            Quaternion lookAtRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * 10);
        }

        #endregion
        
    }
}
