using System.Collections.Generic;
using Core.Currency.Enums;
using Core.Currency.Interfaces;
using UnityEngine;

namespace Core.Currency
{
    public class CurrencyViewModelBinder : MonoBehaviour
    {
        private readonly List<IBindView> _bindViews = new List<IBindView>();
        private CurrencyModel _model;
        private IBindViewModel _viewModel;
        
        private void Awake()
        {
            _model = new CurrencyModel(new Dictionary<CurrencyType, int>()
            {
                { CurrencyType.SOULS, 100 }
            });
            _viewModel = new CurrencyViewModel(_model);
        }
        
        public void Bind(IBindView view)
        {
            if (_bindViews.Contains(view)==false)
            {
                view.Bind(_viewModel);
                _bindViews.Add(view);
            }
        }

        public void Unbind(IBindView view)
        {
            if (_bindViews.Contains(view))
            {
                view.Unbind();
                _bindViews.Remove(view);
            }
        }
    }
}
