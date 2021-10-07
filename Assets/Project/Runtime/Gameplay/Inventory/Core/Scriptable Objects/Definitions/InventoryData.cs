using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Project.Runtime.Gameplay.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Items/Create Inventory")]
    public class InventoryData : ObservableSO
    {
        public IntPair size;
        public List<Item> items;

        #region List Manipulation
        public void AddItem(Item item)
        {
            items.Add(item);
            Notify();
        }
        
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            Notify();
        }

        #endregion

        
    }
}