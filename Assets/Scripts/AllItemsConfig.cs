using Scripts.InventorySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllItemsConfig", menuName = "Configs/AllItemsConfig")]
public class AllItemsConfig : ScriptableObject, IItemStackCapacityProvider
{

    [SerializeField] private List<ItemConfig> _itemConfigs = new List<ItemConfig>();
    public int GetCapacityForItemType(ItemType itemType)
    {
        for(int i = 0; i < _itemConfigs.Count; i++)
        {
            if(itemType == _itemConfigs[i].ItemType)
            {
                return _itemConfigs[i].Capacity;
            }
        }
        throw new Exception();
    }
}
