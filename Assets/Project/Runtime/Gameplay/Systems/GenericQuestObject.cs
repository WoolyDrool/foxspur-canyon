using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Systems
{
    [CreateAssetMenu(menuName = "Game/Quests/New Quest")]
    public class GenericQuestObject : ScriptableObject
    {
        public string QuestName;
        public string QuestDescription;
    }

}