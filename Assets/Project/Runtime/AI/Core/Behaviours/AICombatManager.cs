﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI
{
    public class AICombatManager : MonoBehaviour
    {
        

        [Header("Enemy Stats")]
        public string enemyName;
        public double health;

        [Header("Combat Info")] 
        public LayerMask attackLayers;
        public float minAttackRange = 2;
        public float attackRayLength = 6;
        public Transform raySpawnPoint;

        public float tooFarDistance = 10;

        #region Inernal Variables
        
        internal AIAwareness behaviourSet;
        
        private Ray _damageRay;
        private RaycastHit _damageHit;

        #endregion
        
        public void InitCombat()
        {
            health = behaviourSet.enemyDataSet.baseHealth;
        }

        public void Update()
        {
            _damageRay = new Ray(raySpawnPoint.position, Vector3.forward+transform.position);
            if (health <= 0)
            {
                Debug.Log("Died at " + behaviourSet.currentAIStep.ToString());
                Destroy(gameObject);
                
            }
        }

        public virtual void PongUpdate()
        {
            if (behaviourSet.unitEnabled)
            {
                if (behaviourSet.pathingManager.distanceToPlayer < minAttackRange)
                {
                    behaviourSet.stateMachine.ChangeState(AIStates.STATE_ATTACK);
                }

                if (behaviourSet.pathingManager.distanceToTarget > tooFarDistance)
                {
                    if (behaviourSet.stateMachine.currentState == AIStates.STATE_MOVETOPLAYER)
                    {
                        behaviourSet.stateMachine.ChangeState(AIStates.STATE_CLOSEDISTANCE);
                    }
                }
            }
            
        }

        

        public virtual void TakeDamage(float damageToTake)
        {
            health -= damageToTake;
            Debug.Log("Hit");
        }

        public virtual void Attack(float damage)
        {
            Debug.DrawRay(raySpawnPoint.position, raySpawnPoint.forward, Color.red);
            // Debug: Temp only, remove as soon as finished

            if(Physics.Raycast(_damageRay, out _damageHit, attackRayLength, attackLayers))
            {
                Debug.Log(_damageHit.transform.name);
                _damageHit.transform.SendMessage("TakeDamage", damage, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
