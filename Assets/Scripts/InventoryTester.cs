using Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private AllItemsConfig _allItemsConfig;

    private void Start()
    {
        var inventory = new Inventory(12, _allItemsConfig);
        inventory.AddItems(ItemType.Waterbottle, 3);
        inventory.AddItems(ItemType.Sand, 80);
        Debug.Log(inventory);
    }
}
