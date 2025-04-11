using System.Collections.Generic;
using System.Linq;
using Core.Actions.Interfaces;
using Core.Items.ViewModel.Interfaces;
using Core.Loot;
using Core.Loot.Interfaces;
using Core.Services;
using UnityEngine;

namespace Core
{
    public class PlayerDummyEntity : MonoBehaviour, IActionReceiversOwner
    {
        [SerializeField] private LootCollector _lootCollector;
        private IItemsAmountChanger ItemsAmountChanger =>
            ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsAmountChanger;

        public List<IActionReceiver> LootActionReceivers { get; } = new List<IActionReceiver>(){};
        
        public bool TryGetActionReceiver<T>(out T result) where T : IActionReceiver
        {
            result = (T)LootActionReceivers.FirstOrDefault(x=>x is T);
            return result != null;
        }

        private void Start()
        {
            LootActionReceivers.Add(_lootCollector);
            LootActionReceivers.Add(ItemsAmountChanger);
            _lootCollector.Initialize(this);
        }
    }
}
