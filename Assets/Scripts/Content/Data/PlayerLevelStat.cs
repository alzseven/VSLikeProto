using System;
using UnityEngine;

namespace Content.Data
{
    public class PlayerLevelStat : MonoBehaviour
    {
        [SerializeField] private float[] maxExpByLevel;
        
        public int level;
        public float CurrentExp
        {
            get => _currentExp;
            set
            {
                _currentExp = value;
                if(_currentExp >= _currentLevelMaxExp) LevelUp();
                OnExpChanged?.Invoke(CurrentExp, _currentLevelMaxExp);
            }
        }

        private float _currentExp;
        private float _currentLevelMaxExp;
        public Action<float, float> OnExpChanged;
        public Action<int> OnLevelChanged;


        private void Start() => _currentLevelMaxExp = maxExpByLevel[level];

        private void LevelUp()
        {
            _currentExp -= _currentLevelMaxExp;
            level++;

            if (level >= maxExpByLevel.Length) level = maxExpByLevel.Length - 1;
            
            _currentLevelMaxExp = maxExpByLevel[level];
            OnLevelChanged?.Invoke(level);
            
        }
    }
}