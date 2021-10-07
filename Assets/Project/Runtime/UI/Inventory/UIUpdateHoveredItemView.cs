using Project.Runtime.Gameplay.Inventory;
using TMPro;


namespace Project.Runtime.UI.Elements
{
    public class UIUpdateHoveredItemView : MonoSOObserver
    {
        public ItemObsProp hoveredItemAsset;
        public TextMeshProUGUI nameTxt;
        public TextMeshProUGUI descTxt;
        public TextMeshProUGUI effectsText;

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateView(null);
        }

        public override void Notify()
        {
            UpdateView(hoveredItemAsset.value);
        }

        private void UpdateView(Item item)
        {
            if (item != null)
            {
                nameTxt.text = item.itemName;
                descTxt.text = item.description;
                if (item.effectsDescription != null)
                {
                    effectsText.text = item.effectsDescription;
                }
            }
            else
            {
                nameTxt.text = "";
                descTxt.text = "";
                effectsText.text = "";
            }
        }
    }
}