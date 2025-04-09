using Core.Currency.Enums;
using Core.Currency.Interfaces;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Currency
{
    public class CurrencyView : MonoBehaviour, IBindView
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private Image _icon; //from config?
        
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public void Bind(IBindViewModel viewModel)
        {
            _disposable ??= new CompositeDisposable();
            var currencyViewModel = viewModel as CurrencyViewModel;
            currencyViewModel.GetCurrencyBind(_currencyType).Subscribe(RefreshText).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable?.Dispose();
        }
        
        private void RefreshText(int value)
        {
            _currencyText.text = value.ToString();
        }
    }
}
