using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.AI.States;

namespace Project.Runtime.AI
{
    public struct AIStates
    {
        /// <summary>
        /// Usage: Defines the list of available states
        /// Names are tied explicitly to the StateMachine AnimatorController behaviour names.
        /// </summary>
        public static string STATE_IDLE = "IDLE";
        public static string STATE_WANDER = "WANDER";
        public static string STATE_MOVETOPLAYER = "MOVETOPLAYER";
        public static string STATE_ATTACK = "ATTACK";
        public static string STATE_DIE = "DIE";
    }

    public class AIAwareness : MonoBehaviour
    {
        internal AIPathingManager pathingManager;
        internal AICombatManager combatManager;
        internal AIStateMachine stateMachine;

        public AIEnemyData enemyDataSet;
        public PlayerManager playerManager;
        
        public bool unitEnabled = false;

        [Space(5)]
        [Header("Awareness Settings")]
        public float activationRange = 20;
        public float deactivationRange = 30;

        [Header("Awareness - Line Of Sight")]
        public float viewRadius = 90;
        [Range(0, 360)]
        public float viewAngle = 70;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        [Space(5)]
        [Header("Updates")]
        public float decisionStepInterval = 0.5f;
        public  int currentAIStep;
        public bool trackingPlayer = false;
        bool hasEngagedPlayer = false;

        [Space(5)]
        [Header("Behaviour Randomizer")]
        public float randomizerSpeed = 10;
        public float randomizerChance = 50;

        public virtual void Start()
        {
            playerManager = GameManager.instance.playerManager;
            stateMachine = GetComponent<AIStateMachine>();
            pathingManager = GetComponent<AIPathingManager>();
            combatManager = GetComponent<AICombatManager>();
            combatManager.behaviourSet = this;
            // Prep Agent
            StartCoroutine(IncrementStep());
        }

        public virtual IEnumerator IncrementStep()
        {
            // Usage:
            // Makes update calls to AIPathingManager and AICombatManager
            // This is done to avoid overloading the game with too many 
            // AI Update calls at runtime. Might be refined later.
            // It also updates a value called currentAIStep for tracking and debug purposes.

            currentAIStep++;

            // Ping scripts
            pathingManager.PongUpdate();

            if (pathingManager.distanceToPlayer < activationRange && !unitEnabled)
            {
                ActivateUnit();
            }

            if (pathingManager.distanceToPlayer > deactivationRange && hasEngagedPlayer)
            {
                DeactivateUnit();
            }

            if (unitEnabled)
            {
                combatManager.PongUpdate();
                FindVisibleTargets();
            }

            yield return new WaitForSeconds(decisionStepInterval);
            StartCoroutine(IncrementStep());
            
        }

        #region Initialization
        public virtual void ActivateUnit()
        {
            Debug.LogWarning("Enemy Activated");

            unitEnabled = true;

            stateMachine.EnableStateMachine();
            pathingManager.EnableRepathing(false);
            pathingManager.LockAgent(false);
        }

        public virtual void DeactivateUnit()
        {
            Debug.LogError("Enemy Disabled");

            unitEnabled = false;
            hasEngagedPlayer = false;

            stateMachine.DisableStateMachine();
            pathingManager.DisableRepathing();
            pathingManager.LockAgent(true);
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ActivateUnit();
            }
        }

        #region Line Of Sight
        void FindVisibleTargets()
        {
            //Define a general list of colliders within the view radius
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            //Loop through targetsInViewRadius to find a valid target
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                //If the target is within the view angle
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        if (target.tag == "Player" && !trackingPlayer)
                        {
                            trackingPlayer = true;
                            hasEngagedPlayer = true;
                            stateMachine.ChangeState(AIStates.STATE_MOVETOPLAYER);
                            Debug.Log("Player found at " + currentAIStep.ToString());
                        }
                    }
                }
            }
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        #endregion
    }
}
