using System.Collections.Generic;
using System.Linq;
using Core.Actions.Interfaces;
using Core.Items.ViewModel;
using Core.Loot;
using Core.Loot.Interfaces;
using Core.Progress.ViewModel;
using Core.Services;
using Core.Stats.ViewModel;
using UnityEngine;

namespace Core
{
    public class PlayerDummyEntity : MonoBehaviour, IActionReceiversOwner
    {
        [SerializeField] private LootCollector _lootCollector;
        private ItemsViewModel _itemsViewModel;
        private LevelViewModel _levelViewModel;
        private StatsViewModel _statsViewModel;

        public List<IActionReceiver> LootActionReceivers { get; } = new List<IActionReceiver>(){};

        public void Start()
        {
            _itemsViewModel = ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel;
            _levelViewModel = ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel;
            _statsViewModel = ServiceLocator.Instance.PlayerStatsService.StatsViewModelMediator.StatsViewModel;
            
            LootActionReceivers.Add(_itemsViewModel);
            LootActionReceivers.Add(_levelViewModel);
            LootActionReceivers.Add(_statsViewModel);
            
            _lootCollector.Initialize(this, _statsViewModel);
        }
        
        public bool TryGetActionReceiver<T>(out T result) where T : IActionReceiver
        {
            result = (T)LootActionReceivers.FirstOrDefault(x=>x is T);
            if (result==null)
            {
                Debug.LogWarning($"No action receiver of type {typeof(T)}");
                return false;
            }
            return true;
        }
    }
}
