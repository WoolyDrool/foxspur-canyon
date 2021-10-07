using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableShovelObject : MonoBehaviour
    {
        public int swingsToComplete;
        public int currentSwings;
        public Transform pileObject;
        void Start()
        {
            currentSwings = swingsToComplete;
        }

        void Update()
        {

        }

        public void ProcessDig()
        {
            currentSwings++;
            ChangeState();
        }

        void ChangeState()
        {
            if (currentSwings == 1)
            {
                pileObject.position = new Vector3(pileObject.localPosition.x, pileObject.localPosition.y - 0.5f,
                    pileObject.localPosition.z);
            }

            if (currentSwings == 2)
            {
                pileObject.position = new Vector3(pileObject.localPosition.x, pileObject.localPosition.y - 1f,
                    pileObject.localPosition.z);
            }

            if (currentSwings == 3)
            {
                pileObject.position = new Vector3(pileObject.localPosition.x, pileObject.localPosition.y - 1.5f,
                    pileObject.localPosition.z);
            }

            if (currentSwings == 4)
            {
                Destroy(gameObject);
            }
        }
    }

}