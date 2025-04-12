using System;
using System.Collections.Generic;
using System.Linq;
using Core.Loot.Enums;
using UnityEngine;

namespace Core.Loot.Data
{
    [CreateAssetMenu(menuName = "Game/LootViewConfig", fileName = "LootViewConfig")]
    public class LootViewConfig : ScriptableObject
    {
        [SerializeField] private List<LootViewConfigData> _loots;

        public LootViewConfigData GetLootConfigData(LootType lootType)
        {
            return _loots.FirstOrDefault(x => x.Type == lootType);
        }
        
        [Serializable]
        public class LootViewConfigData
        {
            public LootType Type;
            public Sprite Icon;
        }
    }
}
