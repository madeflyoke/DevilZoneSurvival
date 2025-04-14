using System;
using System.Collections.Generic;
using Core.Items.Enum;
using Core.Items.ViewModel;
using Core.Services;
using Core.Utils;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _disabledView;
        private Action _additionalClickAction;
        private (ItemType, int) _price;
        private ItemsViewModel _callerViewModel;
        private CompositeDisposable _disposable;
        
        public void Setup(Action additionalClickAction, ItemsViewModel callerViewModel, (ItemType, int) price)
        {
            _additionalClickAction = additionalClickAction;
            _priceText.text = FormatterHelper.ConvertToKMB(price.Item2);
            _icon.sprite = ServiceLocator.Instance.ItemsService.ItemsViewConfig.GetItemConfigData(price.Item1).Icon;
            _price = price;
            
            _callerViewModel = callerViewModel;
            
            _disposable ??= new CompositeDisposable();
            _callerViewModel.GetRelatedBind(_price.Item1).Subscribe((x)=>Refresh()).AddTo(_disposable);
        }
        
        public void Show()
        {
            Refresh();
            _button.onClick.AddListener(OnButtonClicked);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
            _disposable?.Dispose();
        }

        private void Refresh()
        {
            _disabledView.SetActive(_callerViewModel.GetRelatedBind(_price.Item1).CurrentValue<_price.Item2);
        }
        
        private void OnButtonClicked()
        {
            _callerViewModel.OnDecreaseAmount(_price.Item1, _price.Item2);
            _additionalClickAction?.Invoke();
        }
    }
}
