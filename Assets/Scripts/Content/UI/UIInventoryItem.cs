using System;
using Content.Data;
using Content.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField] private UIItemLevelImage[] _levelImages;
        [SerializeField] private Image _uiItemImage;
        
        private void Awake() => _levelImages = GetComponentsInChildren<UIItemLevelImage>();

        public void SetImage(Item item)
        {
            _uiItemImage.sprite = item.itemIcon;
            for (var i = 0; i < _levelImages.Length; i++)
            {
                _levelImages[i].SetFill(i <= item.itemLevel);
            }
        }
        
        //TODO: disabled 
    }
}
