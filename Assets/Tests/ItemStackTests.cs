using System;
using NUnit.Framework;
using Scripts.InventorySystem;

namespace Tests
{
    public class ItemStackTests
    {
        private const int DefaultCapacity = 64;

        [Test]
        public void Test_Initialization()
        {
            var itemStack = new ItemStack(DefaultCapacity);
            Assert.IsTrue(itemStack.IsEmpty());
            Assert.AreEqual(0, itemStack.Amount);
            Assert.AreEqual(DefaultCapacity, itemStack.Capacity);
        }

        [Test]
        public void Test_CustomCapacity()
        {
            var itemStack = new ItemStack(128);
            Assert.AreEqual(128, itemStack.Capacity);
        }

        [Test]
        public void Test_AddItems()
        {
            var itemStack = new ItemStack(DefaultCapacity);
            itemStack.AddItems(5);
            Assert.AreEqual(5, itemStack.Amount);
        }

        [Test]
        public void Test_CapacityNull()
        {
            Assert.Throws<Exception>(() => new ItemStack(0));
        }

        [Test]
        public void Test_DropItems()
        {
            var itemStack = new ItemStack(DefaultCapacity);
            itemStack.AddItems(10);
            itemStack.RemoveItems(5);
            Assert.AreEqual(5, itemStack.Amount);
        }

        [Test]
        public void Test_AddItems_ExceedCapacity()
        {
            var addAmount = 100;
            var itemStack = new ItemStack(DefaultCapacity);
            Assert.Throws<Exception>(() => itemStack.AddItems(addAmount));
            Assert.AreEqual(itemStack.Amount, 0);

            itemStack = new ItemStack(DefaultCapacity);
            addAmount = 40;
            itemStack.AddItems(addAmount);
            Assert.Throws<Exception>(() => itemStack.AddItems(addAmount));
            Assert.AreEqual(addAmount, itemStack.Amount);
        }

        [Test]
        public void Test_RemoveItems_ExceedAmount()
        {
            var itemStack = new ItemStack(DefaultCapacity);
            Assert.Throws<Exception>(() => itemStack.RemoveItems(15));
            Assert.AreEqual(0, itemStack.Amount);

            itemStack = new ItemStack(DefaultCapacity);
            var itemsAdded = 10;
            itemStack.AddItems(itemsAdded);
            Assert.Throws<Exception>(() => itemStack.RemoveItems(15));
            Assert.AreEqual(itemsAdded, itemStack.Amount);
        }

        [Test]
        public void Test_GetLeftoverSpace()
        {
            var itemStack = new ItemStack(DefaultCapacity);
            itemStack.AddItems(10);
            var Leftover = itemStack.GetLeftoverSpace();
            Assert.AreEqual(54, Leftover);
        } 
    }
}