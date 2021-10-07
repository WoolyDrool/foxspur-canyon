using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.AI
{
    [CreateAssetMenu(fileName = "New Combat Set", menuName = "AI/Combat Set")]
    public class AIEnemyCombatSet : ScriptableObject
    {
        public List<string> states = new List<string>();
    }
}
