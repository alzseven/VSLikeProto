using System;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class UIKillCountText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            VSLikeProtoGameMode.OnKillCountChanged += i => UpdateText(i.ToString());
        }

        private void UpdateText(string msg) => _text.text = msg;
    }
    
}