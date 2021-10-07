﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Runtime.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIPathingManager : MonoBehaviour
    {
        internal NavMeshAgent agent;

        [SerializeField] internal float distanceToTarget;
        internal Vector3 currentTarget;
        internal Vector3 currentPosition;

        internal Transform playerTransform;
        internal Vector3 playerPosition;
        [SerializeField] internal float distanceToPlayer;
        [SerializeField] AIAwareness behaviourSet;
        
        bool trackingPlayer = false;
        public bool path = false;
        internal bool lookAtPlayer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void InitPathing()
        {
            playerTransform = behaviourSet.playerManager.playerTransform;
            Debug.Log(playerTransform);
            Debug.Log("Current player is " + playerTransform.ToString());
        }

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
                if (path && !trackingPlayer)
                { 
                    
                }
                else if (trackingPlayer)
                {
                    currentTarget = playerPosition;
                    agent.SetDestination(currentTarget);
                }
            }
                     
        }

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

        public void EnableRepathing(bool pathToPlayer)
        {
            // Usage:
            // Enables pathing of the agent globally
            // if pathToPlayer = true, track the player
            // if pathToPlayer = false, do nothing yet
            
            behaviourSet.stateMachine.ChangeState(AIStates.STATE_IDLE);
            
            path = true;
            if (pathToPlayer)
            {
                trackingPlayer = true;
                Debug.Log("Enabled repathing at " + behaviourSet.currentAIStep.ToString());
            }
            else
            {
                trackingPlayer = false;
                return;
            }
        }

        public void DisableRepathing()
        {
            path = false;
            Debug.Log("Disabled repathing at " + behaviourSet.currentAIStep.ToString());
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
            Debug.Log("StopOnPath called at " + behaviourSet.currentAIStep.ToString() + " with a value of " + stop.ToString());

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
            lookAtPlayer = true;
            Vector3 direction = (playerPosition - transform.position).normalized;
            Quaternion lookAtRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * 10);
        }
    }
}
