﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly int _capacity;
        private readonly List<ItemStack> _itemStacks = new List<ItemStack>();

        public IReadOnlyList<ItemStack> ItemStacks
        {
            get { return _itemStacks;  }
        }

        public int Capacity
        {
            get { return _capacity; }
        }

        public Inventory(int capacity)
        {
            if (capacity <= 0)
            {
                throw new Exception("Inventory capacity cannot be 0 (or negative)");
            }

            _capacity = capacity;
        }

        public bool IsEmpty()
        {
            return
                Capacity == 0;
        }

        public void AddItems(ItemType itemType, int itemAmount)
        {
            var itemStack = GetAvailableItemStackForItemType(itemType);
            var remainingCapacity = itemStack.GetRemainingCapacity();
            var itemsToAdd = Math.Min(itemAmount, remainingCapacity);
            itemStack.AddItems(itemsToAdd);
            var remainingItems = itemAmount - itemsToAdd;
            if(remainingItems > 0)
            {
                AddItems(itemType, remainingItems);
            }
        }

        private ItemStack GetAvailableItemStackForItemType(ItemType itemType)
        {
            var itemStack = _itemStacks
                .FirstOrDefault(stack => stack._itemType == itemType && stack.GetRemainingCapacity() > 0);

            return itemStack ?? CreateItemStack(itemType);
        }

        private ItemStack CreateItemStack(ItemType itemType)
        {
            if (_itemStacks.Count < Capacity)
            {
                var itemStack = new ItemStack(itemType);
                _itemStacks.Add(itemStack);
                return itemStack;
            }

            throw new Exception("No more space in inventory!");
        }

        public void AddItemStack(ItemStack itemStack)
        {
            var createdStack = CreateItemStack(itemStack._itemType);
            createdStack.AddItems(itemStack.Amount);
        }

    }
}