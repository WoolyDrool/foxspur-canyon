using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Gameplay.Interactables;

namespace Project.Runtime.Gameplay.Systems
{
    [CreateAssetMenu(menuName = "Game/Quests/New Quest")]
    public class GenericQuestObject : ScriptableObject
    {
        public string questName;
        public string questDescription;
        public QuestType questType;
        public enum QuestType
        {
            TRAIL,
            SIDE,
            MISC
        };

        public QuestStatus questStatus;
        public enum QuestStatus
        {
            NOT_STARTED,
            IN_PROGRESS,
            COMPLETE
        };

        public int currentStage;
        public int numberOfStages;

        public TrailData trailData;

        public void DetermineCurrentStage()
        {
            if (currentStage >= numberOfStages)
            {
                questStatus = QuestStatus.COMPLETE;
            }
        }

        public void StartQuest()
        {
           
        }
        
        public void PauseQuest()
        {}
        
        public void ResumeQuest()
        {}

        public void FinishQuest()
        {}
        
    }
}