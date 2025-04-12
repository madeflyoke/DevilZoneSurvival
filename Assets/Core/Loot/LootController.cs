using System.Collections.Generic;
using System.Linq;
using Core.Actions;
using Core.Actions.Interfaces;
using Core.Loot.Data;
using Core.Loot.Enums;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Loot
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private List<Loot> _lootPrefabVariants;
        private List<Loot> _currentFieldLoots = new List<Loot>();
        
        public void SpawnLoot(Vector3 pos, LootData lootData)
        {
            var prefab = _lootPrefabVariants.FirstOrDefault(x => x.ViewType == lootData.ViewType);
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
                SpawnLoot(Random.insideUnitCircle*5f, new LootData()
                {
                    ViewType = LootType.CURRENCY_SKULL,
                    LootActions = new List<IAction>()
                    {
                        new ItemsCountAppendAction(Items.Enum.ItemType.CURRENCY_SKULL, 99)
                    } 
                });
            }
        }
        
        [Button]
        public void SpawnExp()
        {
            for (int i = 0; i < 30; i++)
            {
                SpawnLoot(Random.insideUnitCircle*10f, new LootData()
                {
                    ViewType = LootType.EXP_LOOT,
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
            SpawnLoot(Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)), new LootData()
                {
                    ViewType = LootType.ALL_MAGNET, 
                    LootActions = new List<IAction>()
                    {
                        new TemporaryMagnetRadiusChangeAction(11, 7)
                    }
                });
        }
    }
}
