using Project.Runtime.Global;
using Project.Runtime.UI.Elements;
using UnityEngine;
using UnityEngine.Audio;

public enum PickupType
{
    CONSUMABLE,
    BATTERY,
    BAG,
    MONEY,
    TRASH
}

namespace Project.Runtime.Gameplay.Inventory
{
    [RequireComponent(typeof(Interactable))]
    public class ItemPickup : MonoBehaviour
    {
        [Header("Information")] 
        public string itemName;
        public PickupType itemType;
        public Item itemToAdd;
        public int itemValue = 0;
        public GameObject model;

        [Header("Interactions")] 
        public AudioClip pickupSound;
        public AudioMixerGroup mixerGroup;
        public PlayerInventory inventory;
        
        #region Internal Variables

        internal Interactable hitbox;
        internal Collider hitboxCollider;

        #endregion

        void Start()
        {
            //Get components
            hitbox = GetComponent<Interactable>();
            hitboxCollider = GetComponent<Collider>();
            inventory = GameManager.instance.playerInventory;

            if (itemToAdd)
            {
                gameObject.name = itemToAdd.itemName;
                hitbox.description = itemToAdd.itemName;
            }
            else
            {
                gameObject.name = itemName;
                hitbox.description = itemName;
            }
        }
        
        public void OnPickup()
        {
            if (itemToAdd)
            {
                if (inventory.TryAddItem(itemToAdd))
                {
                    DoPickup();
                }
                else
                {
                    UIAlertUpdate.alert.AddAlertMessage(AlertType.NOSPACE,"Inventory full!");
                    Debug.LogError("Not picking up");
                    return;
                }
            }
            else
            {
                AddNonItem(itemType);
            }
        }

        public void DoPickup()
        {
            if (itemType == PickupType.CONSUMABLE)
            {
                AddConsumable(); 
                return;
            }

            if (itemType == PickupType.TRASH)
            {
                AddTrash();
                return;
            }
        }

        void AddConsumable()
        {
            GameManager.instance.playerInventory.AddItem(itemToAdd, this);
            GameManager.instance.audioManager.PlaySoundOnce(pickupSound, mixerGroup);
        }

        void AddTrash()
        {
            GameManager.instance.playerInventory.AddItem(itemToAdd, this);
            GameManager.instance.audioManager.PlaySoundOnce(pickupSound, mixerGroup);
        }

        void AddNonItem(PickupType type)
        {
            GameManager.instance.audioManager.PlaySoundOnce(pickupSound, mixerGroup);
            switch (type)
            {
                case PickupType.BATTERY:
                {
                    GameManager.instance.playerInventory.currentBatteries += itemValue;
                    UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMADD, itemValue.ToString() + " Batteries");
                    break;
                }
                case PickupType.BAG:
                {
                    GameManager.instance.playerInventory.currentBags += itemValue;
                    UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMADD, itemValue.ToString() + " Bags");
                    break;
                }
                case PickupType.MONEY:
                {
                    GameManager.instance.playerInventory.currentMoney += itemValue;
                    UIStatusUpdate.update.AddStatusMessage(UpdateType.ITEMADD, itemValue.ToString() + " Money");
                    break;
                }
            }
            
            Destroy(gameObject);
        }
    }

}