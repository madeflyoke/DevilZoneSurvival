using System;
using Core.Items.Data;
using Core.Items.Enum;
using Core.Loot;
using Core.Scripts.Utils;
using EasyButtons;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Items.ViewModel
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private Image _icon;
        
        private CompositeDisposable _disposable;
        
        
        public void Bind(ItemsViewModel itemViewModel)
        {
            _disposable ??= new CompositeDisposable();
            itemViewModel.GetRelatedBind(_itemType).Subscribe(RefreshText).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable?.Dispose();
        }
        
        private void RefreshText(int value)
        {
            _itemCountText.text = value.ToString();
        }
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            var data = Resources.Load<ItemsViewConfig>(Constants.ResourcesPaths.ItemsViewConfig).GetItemConfigData(_itemType);
            if (data==null && _itemType!=ItemType.NONE)
            {
                Debug.LogError($"ItemType {_itemType} not found in config");
                return;
            }

            if (_icon.sprite!=data.Icon)
            {
                _icon.sprite = data.Icon;
            }
        }
#endif
    }
}
