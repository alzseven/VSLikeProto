using System;
using System.Collections.Generic;
using Content.Items;
using Core;
using UnityEngine;

namespace Content.Data
{
    public class PlayerItemInventory : MonoBehaviour
    {
        public List<Item> WeaponItems;
        public List<Item> PassiveItems;
        public Action<List<Item>> OnUpgradeAbleSelected;
        public Action<List<Item>> OnWeaponItemsChanged;

        public Action<List<Item>> OnPassiveItemsChanged;

        // TODO: Should be modified by achievement
        public List<Item> pickableItems;
        [SerializeField] private PlayerLevelStat _playerLevelStat;
        [SerializeField] private PlayerStat _playerStat;
        [SerializeField] private WeaponItem _defaultWeapon;

        private List<Item> _maxLevelWeapons = new();
        private List<Item> _maxLevelPassiveItems = new();

        private void OnEnable() => _playerLevelStat.OnLevelChanged += PickRandomItemsDuringLevelUp;

        private void OnDisable() => _playerLevelStat.OnLevelChanged -= PickRandomItemsDuringLevelUp;

        private void Start() => OnSelectItem(_defaultWeapon);

        private void PickRandomItemsDuringLevelUp(int newLevel)
        {
            List<Item> itemsToUpgrade = new();
            // TODO: other data structure?
            List<Item> availableItems = new();
            
            // TODO: case of only weapon full or only passive full
            if (WeaponItems.Count < 3 || PassiveItems.Count < 3)
            {
                foreach (var weapon in WeaponItems)
                {
                    if (_maxLevelWeapons.Contains(weapon) == false)
                    {
                        availableItems.Add(weapon);
                    }
                }

                foreach (var passiveItem in PassiveItems)
                {
                    if (_maxLevelPassiveItems.Contains(passiveItem) == false)
                    {
                        availableItems.Add(passiveItem);
                    }
                }
                
                //First roll
                if (UnityEngine.Random.Range(0f, 1f) <= 0.3 * (newLevel % 2 == 1 ? 1 : 0))
                {
                    //Random pick one from inventory
                    var randomPicked =
                        RandomPicker
                            .PickFromWeightedRandomCollection(
                                availableItems); //availableItems[Random.Range(0, availableItems.Count)];
                    //Add to Upgrade-ables
                    itemsToUpgrade.Add(randomPicked);
                }

                //Second roll
                if (UnityEngine.Random.Range(0f, 1f) <= 0.3 * (newLevel % 2 == 1 ? 1 : 0))
                {
                    var randomPicked = RandomPicker.PickFromWeightedRandomCollection(availableItems);

                    if (itemsToUpgrade.Contains(randomPicked))
                    {
                        // if upgrade-ables.contains secondary_picked_item
                        // Remove picked_item
                        itemsToUpgrade.Remove(randomPicked);
                    }
                    else
                    {
                        itemsToUpgrade.Add(randomPicked);
                    }
                }

                availableItems.AddRange(pickableItems);

                foreach (var item in itemsToUpgrade) availableItems.Remove(item);

                for (int i = itemsToUpgrade.Count; i < 3; i++)
                {
                    // Random pick one from selectables
                    var randomPicked = RandomPicker.PickFromWeightedRandomCollection(availableItems);
                    // Add picked_item to upgrade-ables
                    itemsToUpgrade.Add(randomPicked);
                    // Remove from selectables
                    availableItems.Remove(randomPicked);
                }
            }
            else
            {
                foreach (var weapon in WeaponItems)
                {
                    if (_maxLevelWeapons.Contains(weapon) == false)
                    {
                        availableItems.Add(weapon);
                    }
                }

                foreach (var passiveItem in PassiveItems)
                {
                    if (_maxLevelPassiveItems.Contains(passiveItem) == false)
                    {
                        availableItems.Add(passiveItem);
                    }
                }
                
                for (int i = itemsToUpgrade.Count; i < 3; i++)
                {
                    // Random pick one from selectables
                    var randomPicked = RandomPicker.PickFromWeightedRandomCollection(availableItems);
                    // Add picked_item to upgrade-ables
                    itemsToUpgrade.Add(randomPicked);
                    // Remove from selectables
                    availableItems.Remove(randomPicked);
                }
            }

            OnUpgradeAbleSelected?.Invoke(itemsToUpgrade);
        }

        public void OnSelectItem(Item target)
        {
            if (WeaponItems.Contains(target) || PassiveItems.Contains(target))
            {
                target.LevelUp(_playerStat);
                //TODO: Level max determine with other way
                if (target.itemLevel == target.levelUpTexts.Count - 1)
                {
                    switch (target)
                    {
                        case WeaponItem:
                            _maxLevelWeapons.Add(target);
                            break;
                        case PassiveItem:
                            _maxLevelPassiveItems.Add(target);
                            break;
                        default:
                            throw new ArgumentException();                        
                    }
                }
            }
            else
            {
                switch (target)
                {
                    case WeaponItem:
                        // TODO: Need fix
                        var weapon = Instantiate(target, GameObject.FindWithTag("Player").transform);
                        WeaponItems.Add(weapon);
                        pickableItems.Remove(target);
                        weapon.Apply(_playerStat);
                        weapon.gameObject.SetActive(true);
                        break;
                    case PassiveItem:
                        PassiveItems.Add(target);
                        pickableItems.Remove(target);
                        target.Apply(_playerStat);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            OnWeaponItemsChanged?.Invoke(WeaponItems);
            OnPassiveItemsChanged?.Invoke(PassiveItems);
        }
    }
}