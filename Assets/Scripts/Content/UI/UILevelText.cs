using System;
using Content.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class UILevelText : MonoBehaviour
    {
        [SerializeField] private PlayerLevelStat _playerLevelStat;
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _playerLevelStat.OnLevelChanged += UpdateText;
        }

        private void UpdateText(int newLevel) => _text.text = $"LV {newLevel} ";
    }
}