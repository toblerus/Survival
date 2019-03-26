using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
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
        new Inventory(0);
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

public class Inventory
{
    public readonly int _size;
    public ItemStack[] contents;
    public int Size { get { return _size; } }

    public Inventory(int size)
    {
        _size = size;
        contents = new ItemStack[size];

        for(int i = 0; i < contents.Length; i++)
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