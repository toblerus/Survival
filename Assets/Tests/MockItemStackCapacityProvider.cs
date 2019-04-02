using Scripts.InventorySystem;

public class MockItemStackCapacityProvider : IItemStackCapacityProvider
{
    public int GetCapacityForItemType(ItemType itemType)
    {
        return 64;
    }
}
