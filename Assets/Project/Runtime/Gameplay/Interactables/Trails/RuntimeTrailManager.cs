using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Inventory;
using TMPro;
using UnityEngine;
using Project.Runtime.Gameplay.Systems;
using Project.Runtime.Global;
using Project.Runtime.UI.Elements;
using UnityEngine.PlayerLoop;

namespace Project.Runtime.Gameplay.Interactables
{
    public enum TrailStatus
    {
        NOT_STARTED,
        JUST_STARTED,
        HALF,
        MINIMUM,
        COMPLETED
    }
    public class RuntimeTrailManager : MonoBehaviour
    {
        [Header("Control")] public bool trailBegan;
        public TrailData trailData;
        public TrailStatus status;
        public GameObject trailContainer;
        public int totalItemsInTrail;
        
        [Header("Score")]
        public int itemsCollected;

        public int secretsFound;
        public float currentTrailScore = 0;
        public float completionPercentage;

        [Header("Payout Settings")] 
        public int trailCompletionPayout;
        public int thoroughnessPayout;
        public int secretsPayout;
        public int survivalPayout;
        public int optionalPayout;
        public int totalPayout;

        [Header("Exit Settings")] 
        public string destinationScene;
        public string destinationName;

        #region Internal Variables

        public string completionPercentageN = "0";
        internal UIElementTrailProgress trailUI;

        #endregion

        private void Awake()
        {
            //DetermineItemCount();
        }

        public void BeginTrail()
        {
            status = TrailStatus.JUST_STARTED;
            trailUI = GameManager.instance.hudManager.trailProgressHud;
            trailUI.Toggle();
            DetermineItemCount();
            UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"Trail Started - WuliKup");
        }

        public void DetermineItemCount()
        {
            //if (trailData.Items == totalItemsInTrail)
            //    return;

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

                //trailData.Items = totalItemsInTrail;
                //percentageText.text = "0/100.0"; 
            }
            else
            {
               // percentageText.text = "No items in trail";
                //percentageText.color = Color.red;
            }
        }

        public void AddScore(int scoreToAdd)
        {
            currentTrailScore = (float) scoreToAdd / totalItemsInTrail;
            itemsCollected += scoreToAdd;
            completionPercentage += currentTrailScore;

            completionPercentageN = (completionPercentage * 100).ToString("F1");
            //percentageText.text = completionPercentageN + "/100.0"; 
            
            UpdateTrailStatus();
            trailUI.UpdateTrailProgressUI(completionPercentage);

            if (itemsCollected == totalItemsInTrail)
            {
                completionPercentage = 1;
                //percentageText.text = "100/100.0"; 
                //percentageText.color = Color.green;
                status = TrailStatus.COMPLETED;
                //trailData.Completed = true;
            }
        }

        void ChangeTrailStatus(TrailStatus newStatus)
        {
            TrailStatus curStatus = status;
            
            if(curStatus == newStatus)
                return;

            status = newStatus;
        }

        public void UpdateTrailStatus()
        {
        }
        
        public void DetermineEligibility()
        {
            if(completionPercentage >= trailData.minimumCompletionPercentage)
            {
                Debug.Log("Trail complete");
                BeginPayoutProcess();
            }
            else
            {
                UpdateTrailStatus();
                UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE,"You have not achieved the minimum completion percentage!");
                trailUI.UpdateTrailProgressUI(completionPercentage);
                //Do rejection stuff
                Debug.LogError("Cant leave trail, have not completed!");
            }
        }
        void BeginPayoutProcess()
        {
            // do stuff later
            
            StartCoroutine(DetermineTotalPayout());
        }

        #region Payout

        IEnumerator DetermineTotalPayout()
        {
            DetermineThoroughnessPayout();
            DetermineSecretsPayout();
            DetermineSurvivalPayout();
            DetermineOptionalPayout();

            totalPayout = (thoroughnessPayout + secretsPayout + survivalPayout + optionalPayout + trailCompletionPayout);

            yield return totalPayout > 0;
            trailUI.FinishTrailProgressUI();
            EventManager.TriggerEvent("FinishTrail", null);
            EventManager.TriggerEvent("FreeMouse", null);
        }

        void DetermineThoroughnessPayout()
        {
            int totalTrailItems = trailData.Items;
            int pickedUp = itemsCollected;
            int notPickedUp = totalTrailItems - pickedUp;

            thoroughnessPayout = (pickedUp - notPickedUp) * 2;

        }

        void DetermineSecretsPayout()
        {
            secretsPayout = secretsFound * 10;
        }

        void DetermineSurvivalPayout()
        {
            //Will add code later...
        }

        void DetermineOptionalPayout()
        {
            //Will add code later
        }

        #endregion
    }
    

}