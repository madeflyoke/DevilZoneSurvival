using Core.Items.ViewModel;
using Core.Progress.ViewModel;
using Core.Rewards;
using Core.Services;
using EasyButtons;
using UnityEngine;

namespace Core.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ItemView _currencyView;
        [SerializeField] private LevelExpView _levelExpView;
        [SerializeField] private LevelUpPopup _levelUpPopup;
        private LevelViewModel _levelViewModel;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var levelViewModel = ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel;
            _levelExpView.Bind(levelViewModel);
            _currencyView.Bind(ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel);
            _levelUpPopup.Initialize();
            _levelUpPopup.Bind(levelViewModel);
        }
        
        private void OnDisable()
        {
           _levelExpView.Unbind();
           _currencyView.Unbind();
           _levelUpPopup.Unbind();
        }
    }
}
