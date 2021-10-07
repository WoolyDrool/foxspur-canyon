using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.UI.Buttons;

namespace Project.Runtime.UI.Elements
{
    public class InventoryDraw : MonoBehaviour
    {
        private UIInventoryView _view;
        public InventoryMath maths;
        public GameObject gridPrefab;
        public GameObject itemPrefab;
        public RectTransform grid;
        public IntPair size;
        public UIClickNotifier notifier;
        private GameObject _itemView;
        internal UIStoredItem storedItem;

        public void WakeUp()
        {
            _view = GetComponent<UIInventoryView>();
            maths = _view.maths;
            size.x = _view.sizeX;
            size.y = _view.sizeY;
            maths.CalcSlotDimensions();
        }

        public void DrawInventory()
        {
            CleanUpGrid();
            DrawGrid();
            HandleItemsInitial();
        }

        #region Grid

        public void DrawGrid()
        {
            GameObject gridCell;
            IntPair unitPair = new IntPair(1, 1);

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    gridCell = Instantiate(gridPrefab, grid);
                    maths.PositionateInGrid(gridCell, new IntPair(i, j), unitPair);
                }
            }
        }
        
        public void CleanUpGrid()
        {
            for (int i = 0; i < grid.childCount; i++)
            {
                Destroy(grid.GetChild(i).gameObject);
            }
        }

        #endregion
        
        private void HandleItemsInitial()
        {
            foreach (UIStoredItem item in _view.itemsInGrid.ToList())
            {
                DrawItem(item);
            }
        }

        private void DrawItem(UIStoredItem uiStoredItem)
        {
            Item item = uiStoredItem.item;
            
            //Position in grid
            _itemView = Instantiate(itemPrefab, grid);
            _itemView.transform.SetParent(grid, false);
            _itemView.name = uiStoredItem.item.itemName;
            maths.PositionateInGrid(_itemView, uiStoredItem.position, item.size);

            //Set Image
            Image img = _itemView.GetComponent<Image>();
            img.sprite = item.icon;
            
            storedItem = uiStoredItem;
            _view.DrawItem(storedItem, _itemView);
        }
        
        public void MoveItem(GameObject gridObj, UIStoredItem item)
        {
            maths.movingObject = gridObj;
            maths.movingItem = item;

            StartCoroutine(ItemMouseFollow());
        }
        
        IEnumerator ItemMouseFollow()
        {
            while (!Input.GetMouseButton(0))
            {
                Vector2 rawMousePosition = Input.mousePosition;
                Vector2 objectOffset = new Vector2(grid.sizeDelta.x - 2, grid.sizeDelta.y - 2);
                Vector2 mousePosition = rawMousePosition - objectOffset;

                    Vector2 relativePosition = 
                    new Vector2(mousePosition.x - grid.sizeDelta.x, 
                        grid.sizeDelta.y + mousePosition.y - grid.sizeDelta.y);

                maths.movingObject.transform.position = new Vector3(relativePosition.x, relativePosition.y, maths.movingObject.transform.position.z);

                yield return null;
                
                _view._canUseItem = maths.movingItem == null;
            }


            _view._canUseItem = true;
            maths.RepositionateMovingObject();
        }
    }
}