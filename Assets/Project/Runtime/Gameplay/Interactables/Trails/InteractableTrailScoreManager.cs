using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Inventory;
using TMPro;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public enum TrailStatus
    {
        NOT_DONE,
        HALF,
        MINIMUM,
        COMPLETED
    }
    public class InteractableTrailScoreManager : MonoBehaviour
    {
        public float currentTrailScore = 0;
        public int totalItemsInTrail;
        public float trailCompletionPercentage;
        private string normalisedCompletionPercentage;
        public GameObject trashContainer;
        public int itemsCollected;
        public TrailStatus status;

        public TextMeshProUGUI percentageText;
        void Start()
        {
            //totalItemsInTrail = trashContainer.transform.childCount;
            if (trashContainer.transform.childCount != 0)
            {
                foreach (Transform i in trashContainer.transform)
                {
                    if (i.TryGetComponent(out InteractableTrailScoreGroup scoreobj))
                    {
                        int score = scoreobj.groupScore;
                        totalItemsInTrail += score;
                    }
                }
                percentageText.text = "0/100.0"; 
            }
            else
            {
                percentageText.text = "No items in trail";
                percentageText.color = Color.red;
            }
        }

        void Update()
        {

        }

        public void AddScore(int scoreToAdd)
        {
            currentTrailScore = (float) scoreToAdd / totalItemsInTrail;
            itemsCollected += scoreToAdd;
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