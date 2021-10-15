using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableTrailScoreGroup : MonoBehaviour
    {
        public Transform groupOfItems;
        public int groupScore;
        
        private float _currentDistanceToPlayer = 0;

        private void Start()
        {

            if (transform.childCount != 0)
            {
                foreach (Transform i in groupOfItems)
                {
                    if (i.TryGetComponent(out InteractableTrailItemScore scoreobj))
                    {
                        int score = scoreobj.itemScore;
                        groupScore += score;
                    }
                }
            }
        }

        private void Update()
        {
            if (groupOfItems != null && groupScore > 0)
            {
                _currentDistanceToPlayer = Vector3.Distance(GameManager.instance.currentPlayerPosition, transform.position);

                if (_currentDistanceToPlayer > 100)
                {
                    if (groupOfItems.gameObject.activeSelf)
                    {
                        groupOfItems.gameObject.SetActive(false);
                        return;
                    }
                }
                else
                {
                    if (!groupOfItems.gameObject.activeSelf)
                    {
                        groupOfItems.gameObject.SetActive(true);
                        return;
                    }
                }
            }
        }
    }

}