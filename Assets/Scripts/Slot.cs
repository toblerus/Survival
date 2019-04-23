using Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot 
{
public readonly ItemStack _itemStack;

    public Slot(ItemStack itemStack)
    {
        _itemStack = itemStack;
    }
}
