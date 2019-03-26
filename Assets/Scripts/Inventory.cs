using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly int _capacity;
        private readonly List<ItemStack> _itemStacks = new List<ItemStack>();

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
                Capacity == 0; //Dürfte eigentlich niemals 0 sein, Inventar wird mit fester ItemSlot Zahl initialisiert
        }

        public List<ItemStack> GetItemStacks()
        {
            throw new NotImplementedException();
        }

        public void AddItems(ItemType itemType, int itemAmount)
        {
            var itemStack = GetAvailableItemStackForItemType(itemType);

            // TODO:
            // 1. find first matching stack (ItemType && Amount < Capacity)
            // 2. add as many items as possible --> remaining items
            // go to 1.
            // if no stack found:
            // 3. check inventory capacity
            // 4. if slot available: create stack
            // 5. add items to new stack
            // if no slot available:
            // 6. exception
        }

        private ItemStack GetAvailableItemStackForItemType(ItemType itemType)
        {
            var itemStack = _itemStacks
                .FirstOrDefault(stack => stack.ItemType == itemType && stack.GetLeftoverSpace() > 0);

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
    }
}