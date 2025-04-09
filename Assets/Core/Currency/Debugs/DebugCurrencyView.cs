using Core.Currency.Enums;
using Core.Currency.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Currency.Debugs
{
    public class DebugCurrencyView : MonoBehaviour, IBindView
    {
        [SerializeField] private int _changedValue;
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;
        
        private CurrencyViewModel _currencyViewModel;
        
        public void Bind(IBindViewModel viewModel)
        {
            _currencyViewModel = viewModel as CurrencyViewModel;
            
            _addButton.onClick.AddListener(AddCurrency);
            _removeButton.onClick.AddListener(RemoveCurrency);
        }

        private void AddCurrency()
        {
            _currencyViewModel.OnIncreaseCurrencyCount(_currencyType, _changedValue);
        }

        private void RemoveCurrency()
        {
            _currencyViewModel.OnDecreaseCurrencyCount(_currencyType, _changedValue);
        }

        public void Unbind()
        {
            _addButton.onClick.RemoveListener(AddCurrency);
            _removeButton.onClick.RemoveListener(RemoveCurrency);
        }
    }
}
