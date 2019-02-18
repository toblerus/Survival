using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsConfig", menuName = "ItemsConfig", order = 1)]
public class MyScriptableObjectClass : ScriptableObject
{
    Dictionary<Items, float> items = new Dictionary<Items, float>();
}