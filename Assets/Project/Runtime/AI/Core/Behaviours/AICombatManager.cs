using System.Collections;
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
        public float maxAttackRange = 6;
        public Transform raySpawnPoint;

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
            if (behaviourSet.pathingManager.distanceToPlayer < minAttackRange)
            {
                behaviourSet.stateMachine.ChangeState(AIStates.STATE_ATTACK);
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

            if(Physics.Raycast(_damageRay, out _damageHit, maxAttackRange, attackLayers))
            {
                GameManager.instance.playerVitals.healthStat.RemoveValue(damage);
                Debug.Log("Attacked at " + behaviourSet.currentAIStep.ToString());
                Debug.Log(_damageHit.transform.name);
                //damageHit.transform.SendMessage("TakeDamage", (double)damage, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
