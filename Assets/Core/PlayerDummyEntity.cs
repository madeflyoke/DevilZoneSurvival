using System.Collections.Generic;
using System.Linq;
using Core.Actions.Interfaces;
using Core.Items.ViewModel;
using Core.Loot;
using Core.Loot.Interfaces;
using Core.Progress.ViewModel;
using Core.Services;
using UnityEngine;

namespace Core
{
    public class PlayerDummyEntity : MonoBehaviour, IActionReceiversOwner
    {
        [SerializeField] private LootCollector _lootCollector;
        private ItemsViewModel _itemsViewModel;
        private LevelViewModel _levelViewModel;

        public List<IActionReceiver> LootActionReceivers { get; } = new List<IActionReceiver>(){};

        public void Start()
        {
            _itemsViewModel = ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel;
            _levelViewModel = ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel;
            
            LootActionReceivers.Add(_lootCollector);
            LootActionReceivers.Add(_itemsViewModel);
            LootActionReceivers.Add(_levelViewModel);
            _lootCollector.Initialize(this);
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
