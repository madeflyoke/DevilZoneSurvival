using System;
using Core.Items.Enum;
using Core.Loot.Data;
using Core.Loot.Enums;
using Core.Scripts.Utils;
using EasyButtons;
using UnityEditor;
using UnityEngine;

namespace Core.Loot
{
    public class LootView : MonoBehaviour
    {
        [SerializeField] private Loot _relatedLoot;
        [SerializeField] private SpriteRenderer _mainIcon;
        [SerializeField] private SpriteRenderer _glow;
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            _relatedLoot ??= GetComponentInParent<Loot>();
            if (_relatedLoot==false || _mainIcon==null)
            {
                return;
            }
            var data = Resources.Load<LootViewConfig>(Constants.ResourcesPaths.LootViewConfig).GetLootConfigData(_relatedLoot.ViewType);
            if (data==null)
            {
                if (_relatedLoot.ViewType!=LootType.NONE)
                {
                    Debug.LogError($"ItemType {_relatedLoot.ViewType} not found in config");
                }
                return;
            }

            if (_mainIcon.sprite != data.Icon)
            {
                _mainIcon.sprite = data.Icon;
                
            }
            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}
