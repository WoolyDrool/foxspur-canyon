using UnityEngine;

namespace Project.Runtime.Gameplay.Inventory
{
    public enum ItemClass
    {
        ITEM,
        TOOL,
        KEY
    }
    [CreateAssetMenu(menuName = "Items/Create Item")]
    public class Item : ScriptableObject
    {
        public GameObject completedItem;
        public ItemClass itemClass;
        public bool isCurrentlyEquipped;
        public bool canBeDropped = true;
        public bool canBeMoved = true;
        public bool canBeUsed = true;
        public string itemName;
        public string description;
        public string effectsDescription;
        public Sprite icon;
        public Sprite img;
        public IntPair size;
        public ItemUse[] use;
        public bool isTrash;
        public AudioClip useSound;
    }
}