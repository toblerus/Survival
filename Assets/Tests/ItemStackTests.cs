using NUnit.Framework;
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
        var addAmount = 100;
        var itemStack = new ItemStack(DefaultCapacity);
        Assert.Throws<Exception>(() => itemStack.AddItems(addAmount));
        Assert.LessOrEqual(itemStack.Amount, DefaultCapacity);
    }

    [Test]
    public void Test_GetLeftoverSpace()
    {
        var itemStack = new ItemStack(DefaultCapacity);
        itemStack.AddItems(10);
        var Leftover = itemStack.GetLeftoverSpace();
        Assert.AreEqual(54, Leftover);
    }

    [Test]
    public void Test_DropTooManyItems()
    {
        var itemStack = new ItemStack(DefaultCapacity);
        itemStack.AddItems(10);
        itemStack.DropItems(15);
        Assert.GreaterOrEqual(itemStack.Amount, 0);
    }

    private void InitializeItemStack(int bar)
    {
        new ItemStack(bar);
    }
}
