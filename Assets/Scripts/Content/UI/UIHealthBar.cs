using System;
using Content.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    [RequireComponent(typeof(Slider))]
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerStat _playerStat;
        private Slider _uiSlider;
        
        private void Awake() => _uiSlider = GetComponent<Slider>();

        private void OnEnable() => _playerStat.OnHealthChanged += UpdateSlider;
        private void OnDisable() => _playerStat.OnHealthChanged -= UpdateSlider;

        private void UpdateSlider(float newCurrentValue, float newMaxValue) => _uiSlider.value = newCurrentValue / newMaxValue;
    }
}