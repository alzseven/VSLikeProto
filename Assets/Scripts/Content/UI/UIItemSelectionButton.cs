using System;
using Content.Data;
using Content.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UI
{
    public class UIItemSelectionButton : MonoBehaviour
    {
        [SerializeField] private PlayerItemInventory _inventory;
        public Item itemToDisplay;
        
        private Button _uiButton;
        private Text _uiDescText;
        private Image _uiIconImage;

        private void Awake()
        {
            _uiButton = GetComponent<Button>();
            _uiButton.onClick.AddListener(OnClickButton);
            _uiDescText = GetComponentInChildren<Text>();
            _uiIconImage = GetComponentsInChildren<Image>()[1];
        }

        public void UpdateItemInfo()
        {
            if (_uiDescText != null)
            {
                _uiDescText.text = itemToDisplay.levelUpTexts[itemToDisplay.itemLevel];
            }

            if (_uiIconImage != null)
            {
                _uiIconImage.sprite = itemToDisplay.itemIcon;
            }
        }
        
        private void Start()
        {
            if(itemToDisplay != null)
                UpdateItemInfo();   
        }

        private void OnClickButton()
        {
            _inventory.OnSelectItem(itemToDisplay);
        }
    }
}