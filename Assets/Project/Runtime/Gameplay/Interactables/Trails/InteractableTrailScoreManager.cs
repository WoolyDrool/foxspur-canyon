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
        [Header("Control")]
        public TrailData trailData;
        public TrailStatus status;
        public GameObject trailContainer;
        public int totalItemsInTrail;
        
        [Space(25)]
        public TextMeshProUGUI percentageText;
        
        [Header("Score")]
        public int itemsCollected;
        public float currentTrailScore = 0;
        public float completionPercentage;

        #region Internal Variables

        private string completionPercentageN;
        
        #endregion
        void Start()
        {
            DetermineItemCount();
        }

        public void DetermineItemCount()
        {
            if (trailData.Items == totalItemsInTrail)
                return;

            if (trailContainer.transform.childCount != 0)
            {
                foreach (Transform i in trailContainer.transform)
                {
                    if (i.TryGetComponent(out InteractableTrailScoreGroup scoreobj))
                    {
                        int score = scoreobj.groupScore;
                        totalItemsInTrail += score;
                    }
                }

                trailData.Items = totalItemsInTrail;
                percentageText.text = "0/100.0"; 
            }
            else
            {
                percentageText.text = "No items in trail";
                percentageText.color = Color.red;
            }
        }

        public void AddScore(int scoreToAdd)
        {
            currentTrailScore = (float) scoreToAdd / totalItemsInTrail;
            itemsCollected += scoreToAdd;
            completionPercentage += currentTrailScore;

            completionPercentageN = (completionPercentage * 100).ToString("F1");
            percentageText.text = completionPercentageN + "/100.0"; 
            
            if (itemsCollected == totalItemsInTrail)
            {
                completionPercentage = 1;
                percentageText.text = "100/100.0"; 
                percentageText.color = Color.green;
            }
        }
    }

}