
using UnityEngine;
[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private ItemType _itemType;
    public ItemType ItemType => _itemType;
    [SerializeField] private int _capacity;
    public int Capacity => _capacity;

}
