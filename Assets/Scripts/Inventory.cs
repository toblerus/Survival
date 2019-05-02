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
            for (int i = 0; i < capacity; i++)
            {
                _slots.Add(new Slot());
            }
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

        public List<Slot> GetFilledSlots()
        {
            return _slots.Where(slot => slot.ItemStack != null).ToList();
        }

        public List<ItemStack> GetItemStacks()
        {
            return GetFilledSlots().Select(slot => slot.ItemStack).ToList();
        }

        private ItemStack GetAvailableItemStackForItemType(ItemType itemType)
        {
            List<Slot> _slotsWithItemStacks = GetFilledSlots();

            var matchingSlot = _slotsWithItemStacks
                .FirstOrDefault(slot => slot.ItemStack.ItemType == itemType && slot.ItemStack.GetRemainingCapacity() > 0);
            if (matchingSlot != null)
            {
                return matchingSlot.ItemStack;
            }
            var emptySlot = GetEmptySlot();
            return CreateItemStack(itemType, emptySlot);
        }

        private Slot GetEmptySlot()
        {
            var emptySlot = _slots.FirstOrDefault(slot => slot.ItemStack == null);
            if (emptySlot == null)
            {
                throw new Exception("No more space in inventory!");
            }
            return emptySlot;
        }

        private ItemStack CreateItemStack(ItemType itemType, Slot slot)
        {
            var itemStack = new ItemStack(itemType, _itemStackCapacityProvider);
            slot.SetItemStack(itemStack);
            return itemStack;
        }

        public void AddItemStack(ItemStack itemStack)
        {
            var createdStack = CreateItemStack(itemStack.ItemType, GetEmptySlot());
            createdStack.AddItems(itemStack.Amount);
        }

        public void SwapSlots(int SlotIndexOne, int SlotIndexTwo)
        {
            if (SlotIndexOne >= _slots.Count || SlotIndexTwo >= _slots.Count)
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