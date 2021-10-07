using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class TrashPickerBag : MonoBehaviour
    {
        public int bagCapacity = 10;
        public int currentCapacity;
        public bool isFull;

        public InteractableThrowableItem bagPrefab;
        public Transform dropPosition;

        public bool canAddItem()
        {
            if (currentCapacity < bagCapacity)
            {
                return true;
            }

            return false;
        }
        public void AddItem()
        {
            if (currentCapacity < bagCapacity && !isFull)
            {
                currentCapacity++;
                if (currentCapacity == bagCapacity)
                {
                    isFull = true;
                }
            }
        }

        public void DropBag()
        {
            if (currentCapacity > 0)
            {
                GameObject clone;
                clone = Instantiate(bagPrefab.gameObject, dropPosition.position, dropPosition.rotation);
                clone.GetComponent<InteractableThrowableItem>().amount = currentCapacity;
                isFull = false;
                currentCapacity = 0;
            }
        }
    }
}