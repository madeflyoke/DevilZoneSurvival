using System.Collections.Generic;
using System.Linq;
using Core.Items.Enum;
using R3;
using UnityEngine;

namespace Core.Items.ViewModel
{
    public class ItemsModel
    {
        public readonly Dictionary<ItemType, ReadOnlyReactiveProperty<int>> Items;
        
        private readonly Dictionary<ItemType, ReactiveProperty<int>> _items;

        public ItemsModel(Dictionary<ItemType, int> items)
        {
            _items = items.ToDictionary(pair=>pair.Key, pair=>new ReactiveProperty<int>(pair.Value));
            Items = _items.ToDictionary(pair => pair.Key, pair => pair.Value.ToReadOnlyReactiveProperty());
        }
        
        public void AddAmount(ItemType itemType, int amount)
        {
            var currentAmount = GetAmount(itemType);
            if (currentAmount!=-1)
            {
                SetAmount(itemType,currentAmount+amount);
            }
        }

        public void RemoveAmount(ItemType itemType, int amount)
        {
            var currentAmount = GetAmount(itemType);
            if (currentAmount!=-1)
            {
                SetAmount(itemType,currentAmount-amount);
            }
        }
        
        public int GetAmount(ItemType itemType)
        {
            if (_items.TryGetValue(itemType, out var amount))
            {
                return amount.Value;
            }
            return -1;
        }
        
        private void SetAmount(ItemType itemType, int amount)
        {
            amount = Mathf.Clamp(amount, 0, int.MaxValue);
            _items[itemType].Value = amount;
        }
    }
}
