using System;
using System.Collections.Generic;

public class Inventory
{
    public Dictionary<ItemType, float> OwnedItems = new Dictionary<ItemType, float>();
    public Inventory ()
    {
        var itemTypes = Enum.GetValues(typeof(ItemType));
        foreach (var itemType in itemTypes)
        {
            OwnedItems.Add((ItemType)itemType, 0f);
        }
    }
}
