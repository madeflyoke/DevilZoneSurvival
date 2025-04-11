using System;
using Core.Items.Data;
using Core.Items.Enum;
using Core.Loot.Data;
using Core.Loot.Interfaces;
using Core.Loot.Magnet;
using UnityEngine;

namespace Core.Loot
{
    public class ItemLoot : MonoBehaviour, IMagnetableLoot
    {
        public event Action Looted;
        public Transform SelfTransform => transform;

        [field: SerializeField] public ItemType ItemType { get; private set; }
        [SerializeField] private ItemLootView _lootView;
        [SerializeField] private Collider2D _collider;
        private ItemLootData _relatedLootData;

        public void Initialize(ItemLootData lootData)
        {
            _relatedLootData = lootData;
        }
        
        public void CallOnMagneted(IItemsLootOwner owner)
        {
            Looted?.Invoke();
            owner.ApplyLoot(_relatedLootData);
            Destroy(gameObject);
        }

        public void CallOnStartMagneting()
        {
            _collider.enabled = false;
        }
    }
}
