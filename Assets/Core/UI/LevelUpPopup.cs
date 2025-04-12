using System;
using System.Collections.Generic;
using Core.Actions;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Progress.ViewModel;
using Core.Rewards;
using Core.Rewards.Data;
using Core.Rewards.Interfaces;
using Core.Services;
using R3;
using UnityEngine;

namespace Core.UI
{
    public class LevelUpPopup : MonoBehaviour
    {
        [SerializeField] private RewardsSelector _rewardsSelector;

        [SerializeField] private int _capacity = 4; //for now
        [SerializeField] private RewardsConfig _rewardsConfig;
        [SerializeField] private RewardElementView _rewardElementViewPrefab;
        [SerializeField] private RectTransform _buffsViewsParent;
        private CompositeDisposable _disposables;
        
        public void Bind(LevelViewModel levelViewModel)
        {
            _disposables ??= new CompositeDisposable();
            levelViewModel.GetLevelBind().Skip(1).Subscribe(x => Show()).AddTo(_disposables);
        }

        public void Unbind()
        {
            _disposables?.Dispose();
        }
        
        private void Initialize()
        {
            var actions = _rewardsSelector.GetRandomRewards(2, _capacity);
            foreach (var action in actions)
            {
                var instance = Instantiate(_rewardElementViewPrefab, _buffsViewsParent);

                var data = _rewardsConfig.GetRelatedRewardData(action);
                
                instance.Initialize(action, data.Icon, data.Title, action.FormatDescription(data.Description));
                instance.Selected += OnBuffSelected;
                instance.Show();
            }
        }
        
        public void Show()
        {
            Initialize();
            gameObject.SetActive(true);
            ServiceLocator.Instance.PauseService.SetPause(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ServiceLocator.Instance.PauseService.SetPause(false);
        }

        private void OnBuffSelected(RewardElementView rewardElementView, IAction action)
        {
            rewardElementView.Selected -= OnBuffSelected;
            action.TryExecute(FindObjectOfType<PlayerDummyEntity>());
            rewardElementView.HideDestroy();
            Hide();
        }
    }
}
