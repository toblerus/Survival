using Scripts.InventorySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllItemsConfig", menuName = "Configs/AllItemsConfig")]
public class AllItemsConfig : ScriptableObject, IItemStackCapacityProvider, IItemStackSpriteProvider
{

    [SerializeField] private List<ItemConfig> _itemConfigs = new List<ItemConfig>();

    public int GetCapacityForItemType(ItemType itemType)
    {
        return GetConfigForItemType(itemType).Capacity;
    }

    public Sprite GetSpriteForItemType(ItemType itemType)
    {
        return GetConfigForItemType(itemType).Sprite;
    }

    private ItemConfig GetConfigForItemType(ItemType itemType)
    {
        for (int i = 0; i < _itemConfigs.Count; i++)
        {
            if (itemType == _itemConfigs[i].ItemType)
            {
                return _itemConfigs[i];
            }
        }
        throw new Exception();
    }


}
