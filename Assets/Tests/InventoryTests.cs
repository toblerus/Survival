using System;
using NUnit.Framework;
using Scripts.InventorySystem;

namespace Tests
{
    public class InventoryTests
    {
        private const int DefaultSlotCapacity = 12;
        private const ItemType DefaultItemType = ItemType.Waterbottle;
        private const int DefaultItemAmount = 30;

        [Test]
        public void Test_Initialization()
        {
            var inventory = new Inventory(DefaultSlotCapacity);
            Assert.AreEqual(DefaultSlotCapacity, inventory.Capacity);
            Assert.AreEqual(0, inventory.GetItemStacks().Count);
        }

        [Test]
        public void Test_CapacityZero_ShouldThrow()
        {
            Assert.Throws<Exception>(() => new Inventory(0));
        }

        [Test]
        public void Test_AddItems()
        {
            var inventory = new Inventory(DefaultSlotCapacity);
            inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var itemStacks = inventory.GetItemStacks();
            Assert.AreEqual(1, itemStacks.Count);
            Assert.AreEqual(DefaultItemAmount, itemStacks[0].Amount);
        }
    }
}