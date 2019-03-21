using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class ItemStackTests {

    private const int DefaultCapacity = 64;

    [Test]
    public void Test_Initialization() {
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
        new ItemStack(0);
        Assert.Throws<Exception>(() => new ItemStack(0));
    }

    [Test]
    public void Test_DropItems()
    {
        var itemStack = new ItemStack(DefaultCapacity);
        itemStack.AddItems(10);
        itemStack.DropItems(5);
        Assert.AreEqual(5, itemStack.Amount);
    }

    [Test]
    public void Test_ExceedCapacity()
    {
        var addAmount = 65;
        var itemStack = new ItemStack(DefaultCapacity);
        Assert.LessOrEqual(addAmount, DefaultCapacity);
    }

    [Test]
    public void Test_GetLeftoverSpace()
    {
        var itemStack = new ItemStack(DefaultCapacity);
        itemStack.AddItems(10);
        var Leftover = itemStack.GetLeftoverSpace();
        Assert.AreEqual(54, Leftover);
    }

    private void InitializeItemStack(int bar)
    {
        new ItemStack(bar);
    }
}

public class ItemStack
{
    private int _amount = 0;
    public int Amount { get { return _amount; } }
    public readonly int Capacity;

    public ItemStack(int capacity)
    {
        Capacity = capacity;
    }

    public bool IsEmpty()
    {
        return _amount == 0;
    }

    public void AddItems(int amount)
    {
        _amount += amount;
    }

    public void DropItems(int amount)
    {
        _amount -= amount;
    }

    public int GetLeftoverSpace()
    {
        return Capacity - _amount;
    }
}