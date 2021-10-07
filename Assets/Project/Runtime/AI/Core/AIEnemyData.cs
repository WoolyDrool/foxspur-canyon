using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "AI/Enemy Data")]
    public class AIEnemyData : ScriptableObject
    {
        [Header("Flavor Text")]
        public string enemyName = "NULL";

        [Space(5)]
        [Header("Health Information")]
        public double baseHealth = 10;

        [Space(5)]
        [Header("Attack Information")]
        public double lightAttack = 5;
        public double normalAttack = 10;
        public double heavyAttack = 20;

    }
}
