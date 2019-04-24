using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly List<Slot> _slots = new List<Slot>();
        private readonly IItemStackCapacityProvider _itemStackCapacityProvider;

        public IReadOnlyList<Slot> Slots => _slots;

        public int Capacity { get; }

        public Inventory(int capacity, IItemStackCapacityProvider itemStackCapacityProvider)
        {
            if (capacity <= 0)
            {
                throw new Exception("Inventory capacity cannot be 0 (or negative)");
            }

            Capacity = capacity;
            _itemStackCapacityProvider = itemStackCapacityProvider;
        }

        public bool IsEmpty()
        {
            return Capacity == 0;
        }

        public void AddItems(ItemType itemType, int itemAmount)
        {
            var itemStack = GetAvailableItemStackForItemType(itemType);
            var remainingCapacity = itemStack.GetRemainingCapacity();
            var itemsToAdd = Math.Min(itemAmount, remainingCapacity);
            itemStack.AddItems(itemsToAdd);
            var remainingItems = itemAmount - itemsToAdd;
            if (remainingItems > 0)
            {
                AddItems(itemType, remainingItems);
            }
        }

        private ItemStack GetAvailableItemStackForItemType(ItemType itemType)
        {
            var slot = _slots
                .FirstOrDefault(stack => stack._itemStack.ItemType == itemType && stack._itemStack.GetRemainingCapacity() > 0);


            return slot?._itemStack ?? CreateItemStack(itemType);
        }

        private ItemStack CreateItemStack(ItemType itemType)
        {
            if (_slots.Count < Capacity)
            {
                var itemStack = new ItemStack(itemType, _itemStackCapacityProvider);
                var slot = new Slot(itemStack);
                _slots.Add(slot);
                return itemStack;
            }

            throw new Exception("No more space in inventory!");
        }

        public void AddItemStack(ItemStack itemStack)
        {
            var createdStack = CreateItemStack(itemStack.ItemType);
            createdStack.AddItems(itemStack.Amount);
        }

        public void SwapSlots(int SlotIndexOne, int SlotIndexTwo)
        {
            if (SlotIndexOne > _slots.Count || SlotIndexTwo > _slots.Count)
            {
                throw new Exception("Tried to swap stacks that are out of bounds!");
            }

            if (SlotIndexOne < 0 || SlotIndexTwo < 0)
            {
                throw new Exception("Swap Index can't be negative!");
            }

            var tempSlot = _slots[SlotIndexOne]; //temporarily save first stack
            _slots[SlotIndexOne] = _slots[SlotIndexTwo]; //make first stack second stack
            _slots[SlotIndexTwo] = tempSlot;//make second stack first stack
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"[{nameof(Inventory)}]");
            builder.AppendLine($"{Slots.Count} / {Capacity} in use:");
            
            foreach (var stack in Slots)
            {
                builder.AppendLine(stack.ToString());
            }

            return builder.ToString();
        }
    }
}