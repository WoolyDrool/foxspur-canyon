using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Inventory;
using UnityEngine;
using UnityEngine.Events;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.Gameplay.Inventory
{
    [System.Serializable]
    public class UpdateItemEvent : UnityEvent<Item>
    {
        
    }

    public class PlayerInventory : MonoBehaviour
    {
        public int availableSlots;
        public List<Item> inventoryItems = new List<Item>();
        public List<Item> trashItems = new List<Item>();
        public InventoryData inventoryData;
        public int currentBatteries;
        public int currentBags;
        public int currentMoney;
        public int currentSecrets;
        public int trashInInventory;
        public int smallTrashinInventory;
        public bool canAddItem = true;

        public List<GameObject> pickups = new List<GameObject>();

        public Transform itemDropPoint;
        [HideInInspector] public ItemPickup itemToPickup;

        [SerializeField] private GameObject itemToDiscard;
        [SerializeField] private Transform _trackedItemContainer;

        [HideInInspector] public UpdateItemEvent updateUIList;
        public UnityEvent resortUIList;
        public UnityEvent updateOccupiedSlots;
        public delegate void RemoveItemEventHandler(Item i);

        public static event RemoveItemEventHandler OnRemove;
        
        public delegate void CheckItemEventHandler(Item i);

        public static event CheckItemEventHandler OnCheck;

        public void Awake()
        {
            //size = inventoryData.size;
            availableSlots = (inventoryData.size.x * inventoryData.size.y);
        }

        public bool TryAddItem(Item item)
        {
            OnCheck?.Invoke(item);
            if (canAddItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddItem(Item item, ItemPickup pickupObj)
        {
            if (pickupObj.itemToAdd != null)
            {
                updateUIList?.Invoke(item);
                if (canAddItem)
                {
                    float itemToAddSize = item.size.x * item.size.y;
                    if (availableSlots > itemToAddSize)
                    {
                        if (item.isTrash)
                        {
                            trashItems.Add(item);
                            trashInInventory++;
                            if (itemToAddSize <= 4)
                            {
                                smallTrashinInventory++;
                            }
                        }
                        else
                        {
                            inventoryItems.Add(item);
                        }
                        TrackItem(pickupObj.gameObject);
                        //availableSlots -= (item.size.x * item.size.y);
                        UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMADD, item.itemName);
                    }
                    else
                    {
                        Debug.LogError("No space!");
                        return;
                    }
                }
            }
        }

        public bool CheckSpace(Item itemToCheck)
        {
            return availableSlots > itemToCheck.size.x * itemToCheck.size.y;
        }

        public void UseItem(Item item)
        {
            Debug.Log("Used " + item.itemName);
            if (item.use != null)
            {
                foreach (ItemUse use in item.use)
                {
                    use.vitals = GameManager.instance.playerVitals;
                    use.OnUse();
                }
                
                //inventorySound.PlayOneShot(item.item.use.useSound);
                RemoveItem(item);
            }
        }

        public void ClearAllTrash()
        {
            foreach (Item i in trashItems)
            {
                UntrackItem(i, true);
                availableSlots += (i.size.x * i.size.y);
            }
            trashItems.Clear();
            resortUIList?.Invoke();
            UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMREMOVE, trashInInventory.ToString() + " Trash");
            trashInInventory = 0;
        }

        public void DropItem(Item item)
        {
            if (item.isTrash)
            {
                trashItems.Remove(item);
                trashInInventory--;
                if ((item.size.x * item.size.y) <= 4)
                {
                    smallTrashinInventory--;
                }
            }
            else
            {
                inventoryItems.Remove(item);
            }
            UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMREMOVE, item.itemName);
            //availableSlots += (item.size.x * item.size.y);
            UntrackItem(item, false);
        }

        public void RemoveItem(Item item)
        {
            if (item.isTrash)
            {
                trashItems.Remove(item);
                trashInInventory--;
                if ((item.size.x * item.size.y) <= 4)
                {
                    smallTrashinInventory--;
                }
            }
            else
            {
                inventoryItems.Remove(item);
            }
            UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMREMOVE, item.itemName);
            availableSlots += (item.size.x * item.size.y);
            OnRemove?.Invoke(item);
            UntrackItem(item, true);
        }

        public void TrackItem(GameObject itemToTrack)
        {
            itemToTrack.transform.SetParent(_trackedItemContainer);
            pickups.Add(itemToTrack);
        }

        public void UntrackItem(Item item, bool alsoDestroy)
        {
            if (itemToDiscard == null)
            {
                for (int i = 0; i < pickups.Count; i++)
                {
                    if (pickups[i].name == item.name)
                    {
                        itemToDiscard = pickups[i].gameObject;
                        DoUntrack(itemToDiscard, alsoDestroy);
                        Debug.Log("Located " + itemToDiscard);
                        itemToDiscard = null;
                    }
                }
            }
        }

        void DoUntrack(GameObject discard, bool alsoDestroy)
        {
            if (discard != null)
            {
                pickups.Remove(discard);
                if (alsoDestroy)
                {
                    //Debug.Log("Destroyed " + itemToDiscard.name);
                    Destroy(discard.gameObject);
                    return;
                }
                Debug.Log("Untracking " + discard.ToString());
                discard.transform.parent = null;
                discard.transform.position = itemDropPoint.transform.position;
            }
        }

        public void RemoveBattery()
        {
            currentBatteries--;
            UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMREMOVE, "Battery");
        }
    }
}
