using UnityEngine;

public interface IItemStackSpriteProvider
{
    Sprite GetSpriteForItemType(ItemType itemType);
}