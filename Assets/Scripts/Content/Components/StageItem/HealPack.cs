using Content.Data;
using UnityEngine;

namespace Content.Components.StageItem
{
    public class HealPack : StageItemComponent
    {
        [SerializeField] private PlayerStat _playerStat;
        public float healAmount;

        protected override void Use() => _playerStat.currentHealth += healAmount;
    }
}