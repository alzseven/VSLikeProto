using System;
using System.Collections.Generic;
using Content.Data;
using Content.Items;
using UnityEngine;

namespace Content.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private PlayerItemInventory _playerItemInventory;
        [SerializeField] private UIInventoryItem[] _uiWeaponItems;
        [SerializeField] private UIInventoryItem[] _uiPassiveItems;

        private void Awake()
        {
            _playerItemInventory.OnWeaponItemsChanged += UpdateWeaponItems;
            _playerItemInventory.OnPassiveItemsChanged += UpdatePassiveItems;
        }

        private void UpdatePassiveItems(List<Item> passiveItems)
        {
            for (var index = 0; index < _uiPassiveItems.Length; index++)
            {
                var uiPassiveItem = _uiPassiveItems[index];
                if (index < passiveItems.Count)
                {
                    uiPassiveItem.SetImage(passiveItems[index]);
                }

                uiPassiveItem.gameObject.SetActive(index < passiveItems.Count);
            }
        }
        
        private void UpdateWeaponItems(List<Item> weaponItems)
        {
            for (var index = 0; index < _uiWeaponItems.Length; index++)
            {
                var uiWeaponItem = _uiWeaponItems[index];
                if (index < weaponItems.Count)
                {
                    uiWeaponItem.SetImage(weaponItems[index]);
                }

                uiWeaponItem.gameObject.SetActive(index < weaponItems.Count);
            }
        }
    }
}