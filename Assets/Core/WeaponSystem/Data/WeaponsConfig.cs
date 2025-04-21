using System;
using System.Collections.Generic;
using System.Linq;
using Core.WeaponSystem.Enums;
using UnityEngine;

namespace Core.WeaponSystem.Data
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Game/WeaponsConfig")]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponConfigData> _weaponsData;

        public WeaponConfigData GetWeaponConfigData(WeaponType weaponType)
        {
            return _weaponsData.FirstOrDefault(x => x.Type == weaponType);
        }
        
        [Serializable]
        public class WeaponConfigData
        {
            public WeaponType Type;
            public Weapon Prefab;
        }
    }
}
