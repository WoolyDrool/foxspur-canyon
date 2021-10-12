using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Gameplay.Inventory;

namespace Project.Runtime.UI.Elements
{
    public class InventoryMath
    {
        private UIInventoryView _view;
        private InventoryDraw _drawing;
        private int _sizeX, _sizeY;
        internal FloatPair _unitSlot;
        internal List<UIStoredItem> gridItems;
        internal RectTransform grid;

        public GameObject movingObject;
        public UIStoredItem movingItem;

        /// <summary>
        /// This class handles all of the calculation and collision functions for the inventory
        /// InventoryDraw and UIInventoryView make frequent calls to this class
        /// All of the math code is relatively unchanged from the tutorial, I have reverted most of the fixes I attempted to make it easier to read
        /// </summary>
        
        public InventoryMath(int sizeX, int sizeY, List<UIStoredItem> inventory, RectTransform gridContainer, UIInventoryView view, InventoryDraw drawer)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            gridItems = inventory;
            grid = gridContainer;
            _view = view;
        }

        #region Grid

        public void CalcSlotDimensions()
        {
            //Get grid dimension
            float gridWidth = grid.rect.width;
            float gridHeight = grid.rect.height;
            
            //Define the dimensions of a singular grid space
            _unitSlot = new FloatPair(gridHeight / _sizeX, gridWidth / _sizeX);
        }

        #endregion

        #region Collision
        public bool ABBintersectsABB(int ax, int ay, float aw, float ah, int bx, int by, float bw, float bh)
        {
            //What the fuck is this....
            return (ax < bx + bw &&
                    ax + aw > bx &&
                    ay <= by + bh &&
                    ah + ay > by);
        }
        
        public int FreeSlotsCount()
        {
            int occupied = 0;
            foreach (UIStoredItem stItem in gridItems)
            {
                occupied += stItem.item.size.x * stItem.item.size.y;
            }

            return _sizeX * _sizeY - occupied;
        }

        public bool IsColliding(IntPair itemSize, int row, int col, UIStoredItem ignoreWith = null)
        {
            foreach (UIStoredItem stItem in gridItems)
            {
                if (ABBintersectsABB(
                        stItem.position.y, stItem.position.x, stItem.item.size.y, stItem.item.size.x, col, row,
                        itemSize.y, itemSize.x)
                    && stItem != ignoreWith)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPositionValid(Item item, int row, int col, UIStoredItem ignoreWith = null)
        {
            return InBounds(item.size, row, col) && !IsColliding(item.size, row, col, ignoreWith);
        }

        //Thank you to Francisco Gázquez for his help in fixing this function
        public bool InBounds(IntPair itemSize, int row, int col)
        {
            return row >= 0 && row < _sizeX &&
                   row + itemSize.x <= _sizeX &&
                   col >= 0 && col < _sizeY &&
                   col + itemSize.y <= _sizeY;
        }
        
        public IntPair FindValidPosition(Item item)
        {
            for (int i = 0; i < _sizeX; i++)
            {
                for (int j = 0; j < _sizeY; j++)
                {
                    if (IsPositionValid(item, i, j))
                    {
                        return new IntPair(i, j);
                    }
                }
            }

            return null;
        }
        
        public FloatPair GetSlotPosition(int row, int col)
        {
            return new FloatPair(row * -_unitSlot.x, col * _unitSlot.y);
        }

        #endregion

        #region Translations

        public void PositionateInGrid(GameObject obj, IntPair position, IntPair size)
        {
            RectTransform rectTransform = obj.transform as RectTransform;
            FloatPair gridPosition = GetSlotPosition(position.x, position.y);

            //Scale Item
            rectTransform.sizeDelta = new Vector2(_unitSlot.y * size.y, _unitSlot.x * size.x);
            
            //Position Item
            rectTransform.localPosition = new Vector3(gridPosition.y, gridPosition.x, 0.0f);
        }

        public void RepositionateMovingObject()
        {
            if (movingObject != null)
            {
                int row = (int) (movingObject.transform.localPosition.y / _unitSlot.x) * -1;
                int col = (int) (movingObject.transform.localPosition.x / _unitSlot.y);

                if (!MoveItemPosition(movingItem, new IntPair(row, col)) && movingItem.item.canBeDropped)
                {
                    _view._canUseItem = false;
                    _view.DropItem(movingItem);
                }
                _view.Notify();
                movingObject = null;
                movingItem = null;
            }
        }
        
        public bool MoveItemPosition(UIStoredItem toMove, IntPair newPos)
        {
            if (IsPositionValid(toMove.item, newPos.x, newPos.y, toMove))
            {
                toMove.position = newPos;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}