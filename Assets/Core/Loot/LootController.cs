using System;
using System.Collections.Generic;
using System.Linq;
using Core.Actions;
using Core.Actions.Interfaces;
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
            var prefab = _lootPrefabVariants.FirstOrDefault(x => x.ItemType == lootData.ItemLootType);
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
                SpawnLoot(Random.insideUnitCircle*5f, new ItemLootData()
                {
                    ItemLootType = ItemType.CURRENCY_SKULL,
                    LootActions = new List<IAction>()
                    {
                        new ItemsCountAppendAction(ItemType.CURRENCY_SKULL, 99)
                    } 
                });
            }
        }
        
        [Button]
        public void SpawnExp()
        {
            for (int i = 0; i < 30; i++)
            {
                SpawnLoot(Random.insideUnitCircle*10f, new ItemLootData()
                {
                    ItemLootType = ItemType.EXP_LOOT,
                    LootActions = new List<IAction>()
                    {
                        new ExpCountAppendAction(Random.Range(50,100))
                    } 
                });
            }
        }

        [Button]
        public void SpawnMagnet()
        {
            SpawnLoot(Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)), new ItemLootData()
                {
                    ItemLootType = ItemType.ALL_MAGNET, 
                    LootActions = new List<IAction>()
                    {
                        new MagnetRadiusChangeAction(11, 7)
                    }
                });
        }
    }
}
