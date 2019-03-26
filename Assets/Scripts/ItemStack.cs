using System;

namespace Scripts.InventorySystem
{
    public class ItemStack
    {
        private int _amount = 0;
        public int Amount { get { return _amount; } }
        public readonly int Capacity;
        public ItemType ItemType;

        public ItemStack(ItemType itemType, int capacity = 64)
        {
            if (capacity <= 0)
            {
                throw new Exception("Capacity cannot be 0 (or negative)");
            }
            ItemType = itemType;
            Capacity = capacity;
        }

        public bool IsEmpty()
        {
            return _amount == 0;
        }

        public void AddItems(int amount)
        {
            if (amount > GetRemainingCapacity())
            {
                throw new Exception("Tried to add more than fits into Stack");
            }
            _amount += amount;
        }

        public void RemoveItems(int amount)
        {
            if (amount <= _amount)
            {
                _amount -= amount;
            }
            else
            {
                throw new Exception("Tried to remove more than left in Stack");
            }
        }

        public int GetRemainingCapacity()
        {
            return Capacity - _amount;
        }
    }
}