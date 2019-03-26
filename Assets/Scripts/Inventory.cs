using System;
using System.Collections.Generic;

namespace Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly int _capacity;
        private ItemStack[] _contents;
        public int Capacity { get { return _capacity; } }

        public Inventory(int capacity)
        {
            if (capacity <= 0)
            {
                throw new Exception("Inventory capacity cannot be 0 (or negative)");
            }

            _capacity = capacity;
            _contents = new ItemStack[capacity];

            for (int i = 0; i < _contents.Length; i++)
            {
                _contents[i] = new ItemStack(ItemType.Waterbottle, 64);
            }
        }

        public bool IsEmpty()
        {
            return Capacity == 0; //Dürfte eigentlich niemals 0 sein, Inventar wird mit fester ItemSlot Zahl initialisiert
        }

        public void AddItemStack(int slot, int amount)
        {
            _contents[slot].AddItems(amount);
        }

        public List<ItemStack> GetItemStacks()
        {
            throw new NotImplementedException();
        }
    }
}