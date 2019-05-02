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
            Assert.AreEqual(DefaultSlotCapacity, _inventory.Slots.Count);
            Assert.AreEqual(0, _inventory.GetItemStacks().Count);
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

            var stacks = _inventory.GetItemStacks();
            Assert.AreEqual(1, stacks.Count);
            Assert.AreEqual(DefaultItemAmount, stacks[0].Amount);
        }

        [Test]
        public void Test_AddItems_MultipleTimes_SingleStack()
        {
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var stacks = _inventory.GetItemStacks();
            Assert.AreEqual(1, stacks.Count);
            Assert.AreEqual(2 * DefaultItemAmount, stacks[0].Amount);
        }

        [Test]
        public void Test_AddItems_MultipleTimes_TwoStacks()
        {
            var itemsAdded = 50;
            _inventory.AddItems(DefaultItemType, itemsAdded);
            _inventory.AddItems(DefaultItemType, itemsAdded);

            var stacks = _inventory.GetItemStacks();
            Assert.AreEqual(2, stacks.Count);
            Assert.AreEqual(ItemStackTests.DefaultCapacity, stacks[0].Amount);
            var expectedAmountSecondStack = 2 * itemsAdded - ItemStackTests.DefaultCapacity;
            Assert.AreEqual(expectedAmountSecondStack, stacks[1].Amount);
        }

        [Test]
        public void Test_SplitStack_AddToInventory()
        {
            _inventory.AddItems(DefaultItemType, DefaultItemAmount);

            var slot = _inventory.Slots[0];
            var newItemStack = slot.ItemStack.Split();
            _inventory.AddItemStack(newItemStack);

            var stacks = _inventory.GetItemStacks();
            Assert.AreEqual(2, stacks.Count);
            var splitItemAmount = DefaultItemAmount / 2;
            Assert.AreEqual(splitItemAmount, stacks[0].Amount);
            Assert.AreEqual(DefaultItemAmount - splitItemAmount, stacks[1].Amount);
        }

        [Test]
        public void Test_AddItems_ExceedInventoryCapacity_ShouldThrow()
        {
            var expectedItemsAdded = DefaultSlotCapacity * ItemStackTests.DefaultCapacity;
            Assert.Throws<Exception>(() => { _inventory.AddItems(DefaultItemType, expectedItemsAdded + 1); });

            Assert.AreEqual(DefaultSlotCapacity, _inventory.Slots.Count);
            var itemSum = _inventory.Slots.Sum(itemStack => itemStack.ItemStack.Amount);
            Assert.AreEqual(expectedItemsAdded, itemSum);
        }

        [Test]
        public void Test_SwapSlots()
        {
            _inventory.AddItems(ItemType.Waterbottle, 1);
            _inventory.AddItems(ItemType.Stone, 80);

            _inventory.SwapSlots(0, 1);

            var slots = _inventory.Slots;
            Assert.AreEqual(ItemType.Stone, _inventory.Slots[0].ItemStack.ItemType); //First Slot should be Stone afterwards, second one should be Water
            Assert.AreEqual(ItemType.Waterbottle, _inventory.Slots[1].ItemStack.ItemType);

            Assert.AreEqual(64, slots[0].ItemStack.Amount); //Amounts should be correct aswell
            Assert.AreEqual(1, slots[1].ItemStack.Amount);
        }

        [Test]
        public void Test_SwapSlots_OutOfBounds_ShouldThrow()
        {
            Assert.Throws<Exception>(() => { _inventory.SwapSlots(0, 12); });
        }

        [Test]
        public void Test_SwapSlots_Negative_ShouldThrow()
        {
            Assert.Throws<Exception>(() => { _inventory.SwapSlots(-1, 12); });
        }
    }
}