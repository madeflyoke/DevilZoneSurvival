using System;
using System.Collections.Generic;
using System.Linq;
using Core.Items.Enum;
using UnityEngine;

namespace Core.Items.Data
{
    [CreateAssetMenu(menuName = "Game/ItemsConfig", fileName = "ItemsConfig")]
    public class ItemsViewConfig : ScriptableObject
    {
        [SerializeField] private List<ItemViewConfigData> _itemTypes;

        public ItemViewConfigData GetItemConfigData(ItemType itemType)
        {
            return _itemTypes.FirstOrDefault(x => x.Type == itemType);
        }
        
        [Serializable]
        public class ItemViewConfigData
        {
            public ItemType Type;
            public Sprite Icon;
        }
    }
}
