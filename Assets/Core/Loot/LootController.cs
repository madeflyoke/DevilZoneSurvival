using System;
using System.Collections.Generic;
using System.Linq;
using Core.Items.Enum;
using Core.Loot.Data;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Loot
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private List<ItemLoot> _lootPrefabVariants;
        private List<ItemLoot> _currentFieldLoots = new List<ItemLoot>();
        
        public void SpawnLoot(Vector3 pos, ItemLootData lootData)
        {
            var prefab = _lootPrefabVariants.FirstOrDefault(x => x.ItemType == lootData.ItemType);
            var instance = Instantiate(prefab, pos, Quaternion.identity);
            instance.Initialize(lootData);
            
            instance.Looted += RemoveLoot;
            _currentFieldLoots.Add(instance);
            
            void RemoveLoot()
            {
                instance.Looted -= RemoveLoot;
                if (_currentFieldLoots.Contains(instance))
                {
                    _currentFieldLoots.Remove(instance);
                }
            }
        }

        [Button]
        public void SpawnCurr()
        {
            for (int i = 0; i < 100; i++)
            {
                SpawnLoot(Random.insideUnitCircle*5f, new ItemLootData(){ItemType = ItemType.CURRENCY_SOUL, Count = 99});
            }
        }
    }
}
