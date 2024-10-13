using System;
using Content.Data;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Content.UI
{
    [RequireComponent(typeof(Slider))]
    public class UIExpBar : MonoBehaviour
    {
        [SerializeField] private PlayerLevelStat _levelStat;
        private Slider _uiSlider;
        [SerializeField] private Image _uiFillImage;

        //rainbow
        //TODO?
        [SerializeField] private float rainbowSpeed;
        private bool invert;
        private float hue;
        public bool shouldChangeColor = false;

        private void Awake()
        {
            _uiSlider = GetComponent<Slider>();

            Assert.IsNotNull(_uiFillImage);
            //set renderermaterial color
        }


        private void OnEnable() => _levelStat.OnExpChanged += UpdateSlider;
        private void OnDisable() => _levelStat.OnExpChanged -= UpdateSlider;

        private void Update()
        {
            if (shouldChangeColor)
            {
                Color.RGBToHSV(_uiFillImage.color, out hue, out var sat, out var bri);
                if (invert)
                {
                    hue -= rainbowSpeed * Time.deltaTime;
                    if (hue < 0) hue = 1f;
                }
                else
                {
                    hue += rainbowSpeed * Time.deltaTime;
                    if (hue > 1) hue = 0f;
                }

                sat = 1;
                bri = 1;
                _uiFillImage.color = Color.HSVToRGB(hue, sat, bri);
            }
        }

        public void ResetColor() => _uiFillImage.color = Color.white;

        private void UpdateSlider(float newCurrentExp, float newMaxExp) => _uiSlider.value = newCurrentExp / newMaxExp;
    }
}