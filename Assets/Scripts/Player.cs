using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemsConfig itemsConfig;

    [SerializeField] private Image waterImage;


    private void Start()
    {
        inventory = new Inventory();
        var itemSettings = itemsConfig.ItemsSettings;
        foreach (var itemSetting in itemSettings)
        {
            if(itemSetting.ItemType == ItemType.Waterbottle)
            {
                waterImage.sprite = itemSetting.Icon;
                break;
            }
        }
    }
}
