using UnityEngine;

namespace Core.Currency.Debugs
{
    public class DebugCurrency : MonoBehaviour
    {
        [SerializeField] private CurrencyViewModelBinder _currencyViewModelBinder;
        [SerializeField] private DebugCurrencyView _debugCurrencyView;

        private void Start()
        {
            _currencyViewModelBinder.Bind(_debugCurrencyView);
        }
    }
}
