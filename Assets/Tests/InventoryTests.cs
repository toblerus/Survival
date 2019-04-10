using System;
using System.Linq;
using NUnit.Framework;
using Scripts.InventorySystem;

namespace Tests
{
    public class InventoryTests
    {
        private const int DefaultSlotCapacity = 12;
        private const ItemType DefaultItemType = ItemType.Waterbottle;
        private const int DefaultItemAmount = 30;

        private Inventory _inventory;
        private MockItemStackCapacityProvider _mockItemStackCapacityProvider;

        [SetUp]
        public void SetUp()
        {
            _mockItemStackCapacityProvider = new MockItemStackCapacityProvider();
            _inventory = new Inventory(DefaultSlotCapacity, _mockItemStackCapacityProvider);
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
            Assert.Throws<Exception>(() => new Inventory(0, _mockItemStackCapacityProvider));
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

        [Test]
        public void Test_AddItems_ExceedInventoryCapacity_ShouldThrow()
        {
            var expectedItemsAdded = DefaultSlotCapacity * ItemStackTests.DefaultCapacity;
            Assert.Throws<Exception>(() => { _inventory.AddItems(DefaultItemType, expectedItemsAdded + 1); });

            Assert.AreEqual(DefaultSlotCapacity, _inventory.ItemStacks.Count);
            var itemSum = _inventory.ItemStacks.Sum(itemStack => itemStack.Amount);
            Assert.AreEqual(expectedItemsAdded, itemSum);
        }

        [Test]
        public void Test_SwapItemStacks()
        {
            _inventory.AddItems(ItemType.Waterbottle, 1);
            _inventory.AddItems(ItemType.Stone, 80);

            _inventory.SwapItemStacks(0, 1);

            var itemStacks = _inventory.ItemStacks;
            Assert.AreEqual(ItemType.Stone, _inventory.ItemStacks[0].ItemType); //First Slot should be Stone afterwards, second one should be Water
            Assert.AreEqual(ItemType.Waterbottle, _inventory.ItemStacks[1].ItemType);

            Assert.AreEqual(64, itemStacks[0].Amount); //Amounts should be correct aswell
            Assert.AreEqual(1, itemStacks[1].Amount);
        }

        [Test]
        public void Test_SwapItemStacks_OutOfBounds_ShouldThrow()
        {
            Assert.Throws<Exception>(() => { _inventory.SwapItemStacks(0, 12); });
        }

        [Test]
        public void Test_SwapItemStacks_Negative_ShouldThrow()
        {
            Assert.Throws<Exception>(() => { _inventory.SwapItemStacks(-1, 12); });
        }
    }
}