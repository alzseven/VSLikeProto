using System.Collections.Generic;
using Content.Data;
using Core;
using UnityEngine;

namespace Content.Items
{
    //TODO: Rename
    public abstract class Item : MonoBehaviour, IWeightedRandomPickable
    {
        public int Weight => weight;
        [SerializeField] private int weight;
        
        public Sprite itemIcon;
        public int itemLevel;
        //TODO: texts + stats?
        public List<string> levelUpTexts;

        public virtual void Apply(PlayerStat stat){}
        public virtual void LevelUp(PlayerStat stat){}
    }
}