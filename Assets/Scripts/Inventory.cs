using System;

namespace Scripts.InventorySystem
{
    public class Inventory
    {
        public readonly int _size;
        public ItemStack[] contents;
        public int Size { get { return _size; } }

        public Inventory(int size)
        {
            if (size <= 0)
            {
                throw new Exception("Inventory Size cannot be 0 (or negative)");
            }

            _size = size;
            contents = new ItemStack[size];

            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = new ItemStack(64);
            }
        }

        public bool IsEmpty()
        {
            return Size == 0; //Dürfte eigentlich niemals 0 sein, Inventar wird mit fester ItemSlot Zahl initialisiert
        }

        public void AddItemStack(int slot, int amount)
        {
            contents[slot].AddItems(amount);
        }
    }
}