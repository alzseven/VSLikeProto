using System;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    [RequireComponent(typeof(Text))]
    public class UITimerText : MonoBehaviour
    {
        private Text _text;

        private void Awake() => _text = GetComponent<Text>();

        //TODO: Opt
        private void Update()
        {
            float minutes = Mathf.FloorToInt( VSLikeProtoGameMode.GameTime / 60f);
            float seconds = Mathf.FloorToInt( VSLikeProtoGameMode.GameTime % 60);
            
            _text.text = $"{minutes}:{seconds:00}";
        }
    }
}