using System;
using Core.Currency;
using UnityEngine;

namespace Core.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private CurrencyViewModelBinder _currencyViewModelBinder;
        [SerializeField] private CurrencyView _soulsCurrencyView;

        private void Start()
        {
            _currencyViewModelBinder.Bind(_soulsCurrencyView);
        }

        private void OnDisable()
        {
            _currencyViewModelBinder.Unbind(_soulsCurrencyView);
        }
    }
}
