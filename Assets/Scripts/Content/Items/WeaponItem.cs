using System.Collections.Generic;
using Content.Data;
using UnityEngine;

namespace Content.Items
{
    public class WeaponItem : Item
    {
        [SerializeField] protected List<WeaponStats> stats;

        // [HideInInspector]
        // public bool statsUpdated;
        
        public override void LevelUp(PlayerStat stat)
        {
            if(itemLevel < stats.Count - 1)
            {
                itemLevel++;

                Apply(stat);
            }
        }
        
        // public override void LevelUp()
        // {
        //     if(itemLevel < stats.Count - 1)
        //     {
        //         itemLevel++;
        //
        //         statsUpdated = true;
        //
        //         if(itemLevel >= stats.Count - 1)
        //         {
        //             //TODO: Case of fully level up items
        //             // PlayerStat.FullUpItems.Add(this);
        //             // PlayerStat.weaponInven.Remove(this);
        //         }
        //     }
        // }
    }
}