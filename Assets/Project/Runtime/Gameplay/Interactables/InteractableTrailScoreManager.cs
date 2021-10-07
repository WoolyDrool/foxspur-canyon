using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Inventory;
using TMPro;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableTrailScoreManager : MonoBehaviour
    {
        public float currentTrailScore = 0;
        public int totalItemsInTrail;
        public float trailCompletionPercentage;
        private string normalisedCompletionPercentage;
        public GameObject trashContainer;
        public int itemsCollected;

        public TextMeshProUGUI percentageText;
        void Start()
        {
            totalItemsInTrail = trashContainer.transform.childCount;
            foreach (Transform i in trashContainer.transform)
            {
                if (i.GetComponent<InteractableLog>())
                {
                    totalItemsInTrail += 2;
                }
            }
            percentageText.text = "0/100.0"; 
        }

        void Update()
        {

        }

        public void AddScore(int scoreToAdd)
        {
            currentTrailScore = (float) scoreToAdd / totalItemsInTrail;
            itemsCollected++;
            trailCompletionPercentage += currentTrailScore;

            normalisedCompletionPercentage = (trailCompletionPercentage * 100).ToString("F1");
            percentageText.text = normalisedCompletionPercentage + "/100.0"; 
            
            if (itemsCollected == totalItemsInTrail)
            {
                trailCompletionPercentage = 1;
                percentageText.text = "100/100.0"; 
                percentageText.color = Color.green;
            }
        }
    }

}