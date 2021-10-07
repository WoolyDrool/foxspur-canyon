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
        public static string STATE_CLOSEDISTANCE = "CLOSETHEDISTANCE";
    }

    public class AIAwareness : MonoBehaviour
    {
        public bool unitEnabled = false;
        [Space(5)]
        
        [Header("References")]
        public PlayerManager playerManager;
        public AIEnemyData enemyDataSet;
        
        [Header("Awareness Settings")]
        public float unitActivationRange = 20;
        public float unitDeactivationRange = 30;

        [Header("Awareness - Line Of Sight")]
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        [Range(0, 360)] public float viewRadius = 90;
        public float viewAngle = 70;
        
        [Header("Updates")]
        public float decisionStepInterval = 0.5f;
        public  int currentAIStep;
        public bool trackingPlayer = false;
        
        [Header("Behaviour Randomizer")]
        public float randomizerSpeed = 10;
        public float randomizerChance = 50;

        #region Internal Variables

        private bool _hasEngagedPlayer = false;
        
        internal AIPathingManager pathingManager;
        internal AICombatManager combatManager; 
        internal AIStateMachine stateMachine;

        #endregion

        private void Awake()
        {
            //Get components
            pathingManager = GetComponent<AIPathingManager>();
            combatManager = GetComponent<AICombatManager>();
            stateMachine = GetComponent<AIStateMachine>();
        }

        public virtual void Start()
        {
            StartCoroutine(IncrementStep());
        }

        /// <summary>
        /// Usage:
        /// Makes update calls to AIPathingManager and AICombatManager
        /// This is done to avoid overloading the game with too many AI update calls at runtime. Might be refined lated
        /// </summary>
        
        public virtual IEnumerator IncrementStep()
        {
            currentAIStep++;
            
            // Ping scripts
            pathingManager.PongUpdate();

            if (pathingManager.distanceToPlayer < unitActivationRange && !unitEnabled)
            {
                ActivateUnit();
            }

            if (pathingManager.distanceToPlayer > unitDeactivationRange && _hasEngagedPlayer)
            {
                DeactivateUnit();
            }

            if (unitEnabled)
            {
                if (pathingManager.distanceToPlayer > 0)
                {
                    combatManager.PongUpdate();
                    FindVisibleTargets();
                }
            }

            yield return new WaitForSeconds(decisionStepInterval);
            StartCoroutine(IncrementStep());
            
        }

        #region Initialization
        public virtual void ActivateUnit()
        {
            Debug.LogWarning("Enemy Activated");
            
            //Initialize pathing
            pathingManager._behaviourSet = this;
            pathingManager.InitPathing();
            pathingManager.EnableRepathing(false);
            pathingManager.LockAgent(false);
            stateMachine.EnableStateMachine();
            
            //Start combat functions
            combatManager.behaviourSet = this;
            combatManager.InitCombat();
            
            playerManager = GameManager.instance.playerManager;
            unitEnabled = true;
        }

        public virtual void DeactivateUnit()
        {
            Debug.LogError("Enemy Disabled");

            unitEnabled = false;
            _hasEngagedPlayer = false;

            stateMachine.DisableStateMachine();
            pathingManager.DisableRepathing();
            pathingManager.LockAgent(true);
        }
        #endregion

        public void BeginStateCooldown(AIBaseBehaviour behaviour)
        {
            StartCoroutine(Cooldown(behaviour.stateCooldown, behaviour));
            
        }

        IEnumerator Cooldown(float cooldown, AIBaseBehaviour behaviour)
        {
            yield return new WaitForSeconds(cooldown);
            behaviour.canEnterState = true;
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
                            _hasEngagedPlayer = true;
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
