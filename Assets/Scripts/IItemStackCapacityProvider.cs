namespace Scripts.InventorySystem
{
    public interface IItemStackCapacityProvider
    {
        int GetCapacityForItemType(ItemType itemType);
    }
}