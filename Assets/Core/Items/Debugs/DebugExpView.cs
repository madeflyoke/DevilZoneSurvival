using System;
using Core.Items.ViewModel;
using Core.Progress.ViewModel;
using Core.Services;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Items.Debugs
{
    public class DebugExpview : MonoBehaviour
    {
        [SerializeField] private int _changedValue;
        [SerializeField] private Button _addButton;
        
        private LevelViewModel _levelViewModel;
        private CompositeDisposable _disposable;

        private void Start()
        {
            Bind(ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel);
        }

        public void Bind(LevelViewModel levelViewModel)
        {
            _levelViewModel = levelViewModel;
            
            _addButton.onClick.AddListener(Add);
            
            _disposable ??= new CompositeDisposable();
        }
        
        public void Unbind()
        {
            _addButton.onClick.RemoveListener(Add);
            _disposable?.Dispose();
        }
        
        private void Add()
        {
            _levelViewModel.OnExpIncreased(_changedValue);
        }
    }
}
