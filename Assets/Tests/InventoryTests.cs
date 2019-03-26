using NUnit.Framework;
using Scripts.InventorySystem;
using System;

public class InventoryTests
{
    private const int DefaultSlotAmount = 12;

    [Test]
    public void Test_Initialization()
    {
        var inventory = new Inventory(DefaultSlotAmount);
        inventory.contents = new ItemStack[DefaultSlotAmount];
        Assert.AreEqual(12, inventory.Size);
    }

    [Test]
    public void Test_SizeNull()
    {
        Assert.Throws<Exception>(() => new Inventory(0));
    }

    [Test]
    public void Test_AddItemStack()
    {
        var inventory = new Inventory(DefaultSlotAmount);
        inventory.AddItemStack(3, 12);
        Assert.AreEqual(12, inventory.contents[3].Amount);
    }
}
