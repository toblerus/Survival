using System;

namespace Scripts.InventorySystem
{
    public class ItemStack
    {
        private int _amount = 0;
        public int Amount { get { return _amount; } }
        public readonly int Capacity;

        public ItemStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new Exception("Capacity cannot be 0 (or negative)");
            }

            Capacity = capacity;
        }

        public bool IsEmpty()
        {
            return _amount == 0;
        }

        public void AddItems(int amount)
        {
            _amount += amount;
        }

        public void RemoveItems(int amount)
        {
            _amount -= amount;
        }

        public int GetLeftoverSpace()
        {
            return Capacity - _amount;
        }
    }
}