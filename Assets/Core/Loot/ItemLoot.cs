using System;
using Core.Items.Enum;
using Core.Loot.Data;
using Core.Loot.Interfaces;
using UnityEngine;

namespace Core.Loot
{
    public class ItemLoot : MonoBehaviour, ICollectableLoot
    {
        public event Action Looted;
        public Transform SelfTransform => transform;

        [field: SerializeField] public ItemType ItemType { get; private set; }
        [SerializeField] private ItemLootView _lootView;
        [SerializeField] private BoxCollider2D _collider;
        private ItemLootData _relatedLootData;

        public void Initialize(ItemLootData lootData)
        {
            _relatedLootData = lootData;
        }
        
        public void CallOnCollected(IActionReceiversOwner actionReceiversOwner)
        {
            Looted?.Invoke();

            foreach (var action in _relatedLootData.LootActions)
            {
                action.TryExecute(actionReceiversOwner);
            }
            Destroy(gameObject);
        }

        public void CallOnStartCollecting()
        {
            _collider.enabled = false;
        }
    }
}
