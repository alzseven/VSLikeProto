using System;
using System.Collections.Generic;

namespace Core
{
    public interface IWeightedRandomPickable
    {
        public int Weight { get; }
    }
    
    //TODO:
    public static class RandomPicker
    {
        public static T PickFromWeightedRandomCollection<T>(List<T> items) where T : IWeightedRandomPickable
        {
            int weightSum = 0;
            foreach (var weightedItem in items)
            {
                if (weightedItem.Weight > 0) weightSum += weightedItem.Weight;
                else throw new ArgumentOutOfRangeException();
            }

            //TODO:
            items.Sort((a,b) => a.Weight.CompareTo(b.Weight));
            int prevSum = 0;
            var rand = UnityEngine.Random.Range(0, weightSum+1);
            foreach (var item in items)
            {
                //TODO:
                if(item.Weight == 0) continue;
                
                if (rand <= prevSum + item.Weight) return item;

                prevSum += item.Weight;
            }

            throw new ArgumentNullException();
        }
    }
}