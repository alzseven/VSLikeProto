using Content.Data;
using UnityEngine;

namespace Content.Items
{
    public class PassiveItem : Item
    {
        [SerializeField] private StatModifier[] _statModifiersByLevel;

        public override void Apply(PlayerStat stat)
        {
            stat.ApplyStatModifier(_statModifiersByLevel[itemLevel]);
        }

        public override void LevelUp(PlayerStat stat)
        {
            if(itemLevel < _statModifiersByLevel.Length - 1)
            {
                stat.RemoveStateModifier(_statModifiersByLevel[itemLevel]);
                itemLevel++;
                Apply(stat);
            }
        }
    }
}