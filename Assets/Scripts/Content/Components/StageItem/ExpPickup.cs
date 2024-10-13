using System.Collections.Generic;
using Content.Data;
using UnityEngine;

namespace Content.Components.StageItem
{
    public class ExpPickup : StageItemComponent
    {
        [SerializeField] private PlayerLevelStat _playerLevelStat;
        public static List<ExpPickup> EnabledExpPickups = new();
        public int expValue;
        
        private void OnEnable() => EnabledExpPickups.Add(this);
        private void OnDisable() => EnabledExpPickups.Remove(this);

        protected override void Use()
        {
            _playerLevelStat = FindObjectOfType<PlayerLevelStat>();
            _playerLevelStat.CurrentExp += expValue;
        }

    }
}