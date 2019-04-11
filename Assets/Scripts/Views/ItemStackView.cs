using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.InventorySystem;

namespace Scripts.Views
{
    public class ItemStackView : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _itemImage;

        public IItemStackSpriteProvider ItemStackSpriteProvider;

        public ItemStack ItemStack;

        void Update()
        {
            if(ItemStack == null)
            {
                return; 
            }
            _amountText.text = ItemStack.Amount.ToString();
            _itemImage.sprite = ItemStackSpriteProvider.GetSpriteForItemType(ItemStack.ItemType);
        }
    }
}