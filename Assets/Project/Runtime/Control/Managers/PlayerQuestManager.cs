using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Gameplay.Systems;

public class PlayerQuestManager : MonoBehaviour
{
    public List<GenericQuestObject> currentQuests = new List<GenericQuestObject>();
    public GenericQuestObject activeQuest;
    public List<GenericQuestObject> completedQuests = new List<GenericQuestObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
