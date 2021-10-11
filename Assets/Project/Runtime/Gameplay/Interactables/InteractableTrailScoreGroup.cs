using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableTrailScoreGroup : MonoBehaviour
    {
        public int groupScore;
        void Start()
        {
            if (transform.childCount != 0)
            {
                foreach (Transform i in transform)
                {
                    if (i.TryGetComponent(out InteractableTrailItemScore scoreobj))
                    {
                        int score = scoreobj.itemScore;
                        groupScore += score;
                    }
                }
            }
        }
    }

}