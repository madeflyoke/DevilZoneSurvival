using Core.Currency.Enums;
using Core.Currency.Interfaces;
using R3;

namespace Core.Currency
{
    public class CurrencyViewModel : IBindViewModel
    {
        private readonly CurrencyModel _currencyModel;

        public CurrencyViewModel(CurrencyModel currencyModel)
        {
            _currencyModel = currencyModel;
        }

        public ReadOnlyReactiveProperty<int> GetCurrencyBind(CurrencyType currencyType)
        {
            return _currencyModel.Currencies[currencyType];
        }
        
        public void OnIncreaseCurrencyCount(CurrencyType currencyType, int amount)
        {
            _currencyModel.AddCount(currencyType, amount);
        }

        public void OnDecreaseCurrencyCount(CurrencyType currencyType, int amount)
        {
            _currencyModel.RemoveCount(currencyType, amount);
        }
    }
}
