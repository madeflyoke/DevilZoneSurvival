using Core.Items.Data;
using Core.Items.Enum;
using Core.Scripts.Utils;
using EasyButtons;
using UnityEditor;
using UnityEngine;

namespace Core.Loot
{
    public class ItemLootView : MonoBehaviour
    {
        [SerializeField] private ItemLoot _relatedItemLoot;
        [SerializeField] private SpriteRenderer _mainIcon;
        [SerializeField] private SpriteRenderer _glow;
        
#if UNITY_EDITOR
        
        [Button]
        private void Setup()
        {
            _relatedItemLoot ??= GetComponentInParent<ItemLoot>();
            var data = Resources.Load<ItemsViewConfig>(Constants.ResourcesPaths.ItemsViewConfig).GetItemConfigData(_relatedItemLoot.ItemType);
            if (data==null && _relatedItemLoot.ItemType!=ItemType.NONE)
            {
                Debug.LogError($"ItemType {_relatedItemLoot.ItemType} not found in config");
                return;
            }
            _mainIcon.sprite = data.Icon;
        }

#endif
    }
}
