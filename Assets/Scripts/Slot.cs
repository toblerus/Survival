using Scripts.InventorySystem;


public class Slot
{
    private ItemStack _itemStack;
    public ItemStack ItemStack { get { return _itemStack; } }

    public Slot()
    {

    }

    public void SetItemStack(ItemStack itemStack)
    {
        _itemStack = itemStack;
    }
}
