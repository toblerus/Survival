using System;

namespace Scripts.InventorySystem
{
    public class ItemStack
    {
        public int Amount { get; private set; } = 0;
        public readonly int Capacity;
        public ItemType ItemType { get; }

        public ItemStack(ItemType itemType, int capacity = 64)
        {
            if (capacity <= 0)
            {
                throw new Exception("Capacity cannot be 0 (or negative)");
            }
            ItemType = itemType;
            Capacity = capacity;
        }

        public ItemStack(ItemType itemType, IItemStackCapacityProvider itemStackCapacityProvider)
            : this (itemType, itemStackCapacityProvider.GetCapacityForItemType(itemType))
        {
        }

        public bool IsEmpty()
        {
            return Amount == 0;
        }

        public void AddItems(int amount)
        {
            if (amount > GetRemainingCapacity())
            {
                throw new Exception("Tried to add more than fits into Stack");
            }
            Amount += amount;
        }

        public void RemoveItems(int amount)
        {
            if (amount <= Amount)
            {
                Amount -= amount;
            }
            else
            {
                throw new Exception("Tried to remove more than left in Stack");
            }
        }

        public int GetRemainingCapacity()
        {
            return Capacity - Amount;
        }

        public ItemStack Split()
        {
            var initialAmount = Amount;
            Amount = Amount / 2;
            var splitStack = new ItemStack(ItemType, Capacity);
            splitStack.AddItems(initialAmount - Amount);
            return splitStack;
        }

        public override string ToString()
        {
            return $"[{nameof(ItemStack)}] {ItemType}: {Amount} / {Capacity}";
        }
    }
}