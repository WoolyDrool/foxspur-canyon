using System.Collections.Generic;
using System.Linq;
using Project.Runtime.Audio;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Global;
using Project.Runtime.UI.Buttons;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace Project.Runtime.UI.Elements
{
    public class UIStoredItem
    {
        public Item item;
        public IntPair position;

        public UIStoredItem(Item _item, IntPair _position)
        {
            item = _item;
            position = _position;
        }
    }
    
    public class UIInventoryView : MonoSOObserver
    {
        public List<UIStoredItem> itemsInGrid = new List<UIStoredItem>();
        public PlayerInventory playerInventory;
        public InventoryData inventoryData;
        
        [Header("Elements")]
        public TextMeshProUGUI batteryCount;
        public TextMeshProUGUI bagsCount;
        public TextMeshProUGUI moneyCount;
        public TextMeshProUGUI secretsCount;

        [Header("Audio")]
        public AudioSource inventorySound;

        #region Internal Varaibles
        
        private FloatPair _unitSlot;

        private int _lastKnownItemCount = 0;

        internal int sizeX;
        internal int sizeY;

        internal bool _canUseItem = true;

        internal InventoryMath maths;
        internal InventoryDraw drawing;
        internal InventorySort sorting;
        private GlobalAudioMixer _audioMixer;

        private UpdateItemEvent u_UpdateEvent;

        

        #endregion

        /// <summary>
        /// The code from your tutorial is mostly the same, but I have separated the functions into disparate classes.
        /// InventoryMath.cs handles all of the collision, intersections, grid math etc
        /// InventoryDraw.cs handles most of the drawing functions like putting the grid/item prefabs on the screen etc
        /// UIInventoryView.cs handles the global functions like adding/removing items, item interactions etc
        /// StoredItem is referred to as UIStoredItem
        /// </summary>
        private void Awake()
        {
            drawing = GetComponent<InventoryDraw>();
            
            playerInventory = GameManager.instance.playerInventory;
            inventoryData = playerInventory.inventoryData;
            
            // Size is 10x10
            sizeX = inventoryData.size.x;
            sizeY = inventoryData.size.y;

            playerInventory.updateUIList.AddListener(UpdateItems);
            playerInventory.resortUIList.AddListener(ResortItems);
            //u_UpdateEvent.AddListener(UpdateItems);
            PlayerInventory.OnRemove += RemoveSingleTrashItem;

            //Initializes the maths and drawing classes
            maths = new InventoryMath(sizeX, sizeY, itemsInGrid, drawing.grid, this, drawing);
            drawing.WakeUp();

            //sorting = new InventorySort(this, maths);
            
            foreach (Item i in playerInventory.inventoryItems)
            {
                AddItem(i);
            }
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            drawing.DrawInventory();
            batteryCount.text = playerInventory.currentBatteries.ToString();
            bagsCount.text = playerInventory.currentBags.ToString();
            moneyCount.text = playerInventory.currentMoney.ToString();
            secretsCount.text = playerInventory.currentSecrets.ToString();
        }

        public override void Notify()
        {
            drawing.DrawInventory();
        }
        
        public void HandleItemInteraction(UIStoredItem uiStoredItem, GameObject obj)
        {
            UpdateHoveredItem hovUpdater = obj.GetComponent<UpdateHoveredItem>();
            hovUpdater.item = drawing.storedItem.item;

            UIClickNotifier notifier = hovUpdater.GetComponent<UIClickNotifier>();
            
            if(_canUseItem)
            {
                notifier.onLeft.AddListener(
                () =>
                {
                    UseItem(uiStoredItem);
                }
                );
            }
            
            notifier.onRight.AddListener(
                () =>
                {
                    if (maths.movingItem == null && drawing.storedItem.item.canBeMoved)
                    {
                        Debug.Log("Moving item");
                        drawing.MoveItem(obj, uiStoredItem);
                    }
                    
                });
        }

        #region Item Interactions
        public void AddItem(Item item)
        {
            int totalSize = item.size.x * item.size.y;

            if (maths.FreeSlotsCount() >= totalSize)
            {
                IntPair position = maths.FindValidPosition(item);
                if (position != null)
                {
                    _lastKnownItemCount++;
                    itemsInGrid.Add(new UIStoredItem(item, position));
                    playerInventory.availableSlots = maths.FreeSlotsCount();
                    Notify();
                    //drawing.AddNewItem(itemsToBeSorted);
                }
            }
            else
            {
                EventManager.TriggerEvent("cantAddItem", null);
            }
        }
        
        public void UseItem(UIStoredItem item)
        {
            Debug.Log(item.item.ToString());
            if (_canUseItem && item.item.canBeUsed)
            {
                playerInventory.UseItem(item.item);
                GameManager.instance.audioManager.PlayInterfaceSound(item.item.useSound);
                RemoveItem(item);
            }
        }

        public void DropItem(UIStoredItem item)
        {
            if (item.item.canBeDropped)
            {
                playerInventory.DropItem(item.item);
                //inventory.RemoveItem(item);
                RemoveItem(item);
                //_playerInventory.RemoveItem(item.item);
            }
            else
            {
                maths.RepositionateMovingObject();
            }
        }

        public void RemoveItem(UIStoredItem item)
        {
            itemsInGrid.Remove(item);

            maths.movingObject = null;
            maths.movingItem = null;

            maths.RepositionateMovingObject();
            Notify();
        }
        
        private void UpdateItems(Item newItem)
        {
            //This might not be the most elegant solution but fuck it
            
            AddItem(newItem);
            
            /*itemsInGrid.Clear();
            
            foreach (Item i in playerInventory.inventoryItems)
            {
                AddItem(i);
            }
            
            foreach (Item i in playerInventory.trashItems)
            {
                AddItem(i);
            }*/
            
        }

        private void RemoveSingleTrashItem(Item itemToRemove)
        {
            UIStoredItem item2 = itemsInGrid.Find(item => itemToRemove);
            itemsInGrid.Remove(item2);
            Notify();
        }

        private void ResortItems()
        {
           
            
            itemsInGrid.Clear();
            
            foreach (Item i in playerInventory.inventoryItems)
            {
                AddItem(i);
            }
            
            foreach (Item i in playerInventory.trashItems)
            {
                AddItem(i);
            }
        }

        #endregion
    }
}