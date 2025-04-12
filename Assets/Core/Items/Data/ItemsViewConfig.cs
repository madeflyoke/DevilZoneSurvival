using System;
using System.Collections.Generic;
using System.Linq;
using Core.Items.Enum;
using UnityEngine;

namespace Core.Items.Data
{
    [CreateAssetMenu(menuName = "Game/ItemsConfig", fileName = "ItemsViewConfig")]
    public class ItemsViewConfig : ScriptableObject
    {
        [SerializeField] private List<ItemsViewConfigData> _itemTypes;
        public ItemsViewConfigData GetItemConfigData(ItemType itemType)
        {
            return _itemTypes.FirstOrDefault(x => x.Type == itemType);
        }
        
        [Serializable]
        public class ItemsViewConfigData
        {
            public ItemType Type;
            public Sprite Icon;
        }
    }
}
