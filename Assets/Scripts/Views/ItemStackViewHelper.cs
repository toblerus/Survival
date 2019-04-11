using Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Views
{
    [RequireComponent(typeof(ItemStackView))]
    public class ItemStackViewHelper : MonoBehaviour
    {
        private ItemStack _itemStack;
        private ItemStackView _itemStackView;
        [SerializeField] private AllItemsConfig _allItemsConfig;
  
        void Awake()
        {
            _itemStackView = GetComponent<ItemStackView>();
            InitializeItemStack(ItemType.Waterbottle);
            _itemStackView.ItemStackSpriteProvider = _allItemsConfig;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                _itemStack.AddItems(1);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                _itemStack.RemoveItems(1);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                InitializeItemStack(ItemType.CanOfBeans);
            }
        }

        private void InitializeItemStack(ItemType itemType)
        {
            _itemStack = new ItemStack(itemType, _allItemsConfig);
            _itemStackView.ItemStack = _itemStack;
        }
    }
}