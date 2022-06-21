using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Project.Runtime.Gameplay.Inventory
{
    public class UpdateHoveredItem : MonoBehaviour
    {
        public ItemObsProp hoveredItemAsset;
        public Item item;
        public TextMeshProUGUI equippedText;
        public Image previewImage;
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