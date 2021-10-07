using System;
using TMPro;
using UnityEngine;

namespace Project.Runtime.Gameplay.Inventory
{
    public class UpdateHoveredItem : MonoBehaviour
    {
        public ItemObsProp hoveredItemAsset;
        public Item item;
        public TextMeshProUGUI equippedText;
        public int currentEquipSlot;

        private void OnEnable()
        {
            if (item == null)
                return;
            
            if (item.itemClass == ItemClass.TOOL)
            {
                equippedText.text = currentEquipSlot.ToString();
            }
        }

        private void OnDisable()
        {
            if (hoveredItemAsset.value == item)
            {
                hoveredItemAsset.value = null;
            }
        }

        public void OnMouseEnter()
        {
            hoveredItemAsset.value = item;
        }

        public void OnMouseExit()
        {
            hoveredItemAsset.value = null;
        }
    }
}