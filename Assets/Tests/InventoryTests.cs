using System;
using NUnit.Framework;
using Scripts.InventorySystem;

namespace Tests
{
    public class InventoryTests
    {
        private Inventory _inventory;
        private const int DefaultSlotCapacity = 12;
        private const ItemType DefaultItemType = ItemType.Waterbottle;
        private const int DefaultItemAmount = 30;

        [SetUp]
        public void SetUp()
        {
            _inventory = new Inventory(DefaultSlotCapacity);
        }

        [Test]
        public void Test_Initialization()
        {
            Assert.AreEqual(DefaultSlotCapacity, _inventory.Capacity);
            Assert.AreEqual(0, _inventory.ItemStacks.Count);
        }

        [Test]
        public void Test_CapacityZero_ShouldThrow()
        {
            Assert.Throws<Exception>(() => new Inventory(0));
        }

        [Test]
        public void Test_AddItems()
        {
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var itemStacks = _inventory.ItemStacks;
            Assert.AreEqual(1, itemStacks.Count);
            Assert.AreEqual(DefaultItemAmount, itemStacks[0].Amount);
        }

        [Test]
        public void Test_AddItems_MultipleTimes_SingleStack()
        {
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var itemStacks = _inventory.ItemStacks;
            Assert.AreEqual(1, itemStacks.Count);
            Assert.AreEqual(2 * DefaultItemAmount, itemStacks[0].Amount);
        }

        [Test]
        public void Test_AddItems_MultipleTimes_TwoStacks()
        {
            var itemsAdded = 50;
            _inventory.AddItems(DefaultItemType, itemsAdded);
            _inventory.AddItems(DefaultItemType, itemsAdded);

            var itemStacks = _inventory.ItemStacks;
            Assert.AreEqual(2, itemStacks.Count);
            Assert.AreEqual(ItemStackTests.DefaultCapacity, itemStacks[0].Amount); 
            var expectedAmountSecondStack = 2 * itemsAdded - ItemStackTests.DefaultCapacity;
            Assert.AreEqual(expectedAmountSecondStack, itemStacks[1].Amount);
        }

        [Test]
        public void Test_SplitStack_AddToInventory()
        {
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var itemStack = _inventory.ItemStacks[0];
            var newItemStack = itemStack.Split();
            _inventory.AddItemStack(newItemStack);

            var itemStacks = _inventory.ItemStacks;
            Assert.AreEqual(2, itemStacks.Count);
            var splitItemAmount = DefaultItemAmount / 2;
            Assert.AreEqual(splitItemAmount, itemStacks[0].Amount);
            Assert.AreEqual(DefaultItemAmount - splitItemAmount, itemStacks[1].Amount);
        }
    }
}