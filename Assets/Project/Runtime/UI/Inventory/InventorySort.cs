using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.Runtime.Gameplay.Inventory;


namespace Project.Runtime.UI.Elements
{
    public class InventorySort
    {
        public List<UIStoredItem> storedItems = new List<UIStoredItem>();
        public List<UIStoredItem> newItems = new List<UIStoredItem>();
        public List<UIStoredItem> combinedList;

        private InventoryMath _math;
        private UIInventoryView _view;

        private IntPair _inventorySize;
        private IntPair _availableSlots;

        public InventorySort (UIInventoryView view, InventoryMath math)
        {
            _view = view;
            _math = math;

            _inventorySize.x = _view.sizeX;
            _inventorySize.y = _view.sizeY;
        }
        
        public bool CheckForSpace(UIStoredItem itemToCheck)
        {
            int compSize = itemToCheck.item.size.x * itemToCheck.item.size.y;
            if (compSize > _math.FreeSlotsCount())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InjectNewItem(UIStoredItem newItem)
        {
            newItems.Add(newItem);

            var oldList = storedItems;
            var newList = newItems;
            combinedList = newList.Except(oldList).ToList();
            
        }
        
        void SortNewItem()
        {}
        
        public void RemoveItem(UIStoredItem item)
        {}
        
        public void SortInventory()
        {}
        
    }
}