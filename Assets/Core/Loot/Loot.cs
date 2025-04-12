using System;
using Core.Loot.Data;
using Core.Loot.Enums;
using Core.Loot.Interfaces;
using UnityEngine;

namespace Core.Loot
{
    public class Loot : MonoBehaviour, ICollectableLoot
    {
        public event Action Looted;
        public Transform SelfTransform => transform;

        [field: SerializeField] public LootType ViewType { get; private set; }
        [SerializeField] private LootView _lootView;
        [SerializeField] private BoxCollider2D _collider;
        private LootData _relatedLootData;

        public void Initialize(LootData lootData)
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
