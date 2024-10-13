using System;
using System.Collections.Generic;
using System.Linq;
using Content.Data;
using Content.Items;
using Core;
using UnityEngine;

namespace Content.UI
{
    //TODO: Separate logics
    public class UILevelUpPanel : MonoBehaviour
    {
        [SerializeField] private PlayerItemInventory _itemInventory;
        //TODO: Move to somewhere else
        [SerializeField] private UIExpBar _expBar;
        [SerializeField] private GameObject _levelUpVfxObj;
        
        public List<UIItemSelectionButton> uiItemSelectionButtons;

        private void Awake()
        {
            uiItemSelectionButtons = new List<UIItemSelectionButton>(GetComponentsInChildren<UIItemSelectionButton>());

            _itemInventory.OnUpgradeAbleSelected += Show;
            gameObject.SetActive(false);
        }


        public void Show(List<Item> items)
        {
            gameObject.SetActive(true);
            
            GameInstance.IsGamePaused = true;

            _expBar.shouldChangeColor = true;
            _levelUpVfxObj.SetActive(true);

            for (var i = 0; i < uiItemSelectionButtons.Count; i++)
            {
                if (i < items.Count)
                {
                    uiItemSelectionButtons[i].itemToDisplay = items[i];
                    uiItemSelectionButtons[i].UpdateItemInfo();
                }
                uiItemSelectionButtons[i].gameObject.SetActive(i < items.Count);                
            }
        }
        
        public void Hide()
        {
            GameInstance.IsGamePaused = false;
            _expBar.shouldChangeColor = false;
            _expBar.ResetColor();
            _levelUpVfxObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}