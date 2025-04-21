using System;
using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Items.Enum;
using Core.Progress.ViewModel;
using Core.Rewards;
using Core.Rewards.Data;
using Core.Services;
using Core.Units.Components.ModelComponents;
using Core.Units.UnitBrains;
using R3;
using UnityEngine;

namespace Core.UI
{
    public class LevelUpPopup : MonoBehaviour
    {
        [SerializeField] private int _capacity = 4; //for now
        [SerializeField] private RewardsConfig _rewardsConfig;
        [SerializeField] private RewardElementView _rewardElementViewPrefab;
        [SerializeField] private RectTransform _buffsViewsParent;
        [SerializeField] private BuyButton _refreshButton;
        private CompositeDisposable _disposables;
        private RewardsSelector _rewardsSelector;

        private List<RewardElementView> _currentRewardViews;

        public void Initialize()
        {
            _rewardsSelector = new RewardsSelector();
        }

        public void Bind(LevelViewModel levelViewModel)
        {
            _disposables ??= new CompositeDisposable();
            levelViewModel.GetLevelBind().Skip(1).Subscribe(x => Show()).AddTo(_disposables);
        }

        public void Unbind()
        {
            _disposables?.Dispose();
        }
        
        private void SetRewards()
        {
            _currentRewardViews = new List<RewardElementView>();
            var actions = _rewardsSelector.GetRandomRewards(2, _capacity);
            foreach (var action in actions)
            {
                var instance = Instantiate(_rewardElementViewPrefab, _buffsViewsParent);

                var data = _rewardsConfig.GetRelatedRewardData(action);
                
                instance.Initialize(action, data.Icon, data.Title, action.FormatDescription(data.Description));
                instance.Selected += OnBuffSelected;
                instance.Show();
                _currentRewardViews.Add(instance);
            }
        }
        
        private void Show()
        {
            ServiceLocator.Instance.PauseService.SetPause(true);

            SetupRefreshButton((ItemType.CURRENCY_SKULL, 196853)); //TODO Get from config or smth
            _refreshButton.Show();
            SetRewards();
            gameObject.SetActive(true);
        }
        
        private void SetupRefreshButton((ItemType, int) refreshPrice)
        {
            var itemViewModel = ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel;
            _refreshButton.Setup(RefreshRewards, itemViewModel, refreshPrice);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _refreshButton.Hide();
            Clear();
            ServiceLocator.Instance.PauseService.SetPause(false);
        }

        private void RefreshRewards()
        {
            Clear();
            SetRewards();
        }
        
        private void Clear()
        {
            foreach (var element in _currentRewardViews)
            {
                element.Selected -= OnBuffSelected;
                element.HideDestroy();
            }
        }

        private void OnBuffSelected(IAction action)
        {
            action.TryExecute(FindObjectOfType<PlayerBrain>().GetUnitComponent<ViewModelsHolderComponent>());
            Hide();
        }
    }
}
