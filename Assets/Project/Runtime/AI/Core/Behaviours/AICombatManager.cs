using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI
{
    public class AICombatManager : MonoBehaviour
    {
        internal AIAwareness behaviourSet;

        [Header("Enemy Stats")]
        public string enemyName;
        public double health;

        [Header("Combat Info")]
        public float minAttackRange = 2;
        public float maxAttackRange = 6;

        Ray damageRay;
        RaycastHit damageHit;
        public Transform raySpawnPoint;


        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<AIAwareness>().enemyDataSet.baseHealth;
        }

        public void Update()
        {
            damageRay = new Ray(raySpawnPoint.position, Vector3.forward+transform.position);
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
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            
            if(Physics.Raycast(damageRay, out damageHit, maxAttackRange))
            {
                Debug.Log("Attacked at " + behaviourSet.currentAIStep.ToString());
                Debug.Log(damageHit.transform.name);
                damageHit.transform.SendMessage("TakeDamage", (double)damage, SendMessageOptions.RequireReceiver);
                
            }
        }
    }
}
