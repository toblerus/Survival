using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsConfig", menuName = "ItemsConfig", order = 1)]
public class ItemsConfig : ScriptableObject
{
    public List<ItemSetting> ItemsSettings = new List<ItemSetting>();
}