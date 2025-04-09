using System.Collections.Generic;
using System.Linq;
using Core.Currency.Enums;
using R3;
using UnityEngine;

namespace Core.Currency
{
    public class CurrencyModel
    {
        public readonly Dictionary<CurrencyType, ReadOnlyReactiveProperty<int>> Currencies;
        
        private readonly Dictionary<CurrencyType, ReactiveProperty<int>> _currencies;

        public CurrencyModel(Dictionary<CurrencyType, int> currencies)
        {
            _currencies = currencies.ToDictionary(pair=>pair.Key, pair=>new ReactiveProperty<int>(pair.Value));
            Currencies = _currencies.ToDictionary(pair => pair.Key, pair => pair.Value.ToReadOnlyReactiveProperty());
        }
        
        public void AddCount(CurrencyType currencyType, int amount)
        {
            var currentCount = GetCount(currencyType);
            if (currentCount!=-1)
            {
                SetCount(currencyType,currentCount+amount);
            }
        }

        public void RemoveCount(CurrencyType currencyType, int amount)
        {
            var currentCount = GetCount(currencyType);
            if (currentCount!=-1)
            {
                SetCount(currencyType,currentCount-amount);
            }
        }
        
        public int GetCount(CurrencyType currencyType)
        {
            if (_currencies.TryGetValue(currencyType, out var count))
            {
                return count.Value;
            }
            return -1;
        }
        
        private void SetCount(CurrencyType currencyType, int amount)
        {
            amount = Mathf.Clamp(amount, 0, int.MaxValue);
            _currencies[currencyType].Value = amount;
        }
    }
}
