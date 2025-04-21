using System;
using Core.WeaponSystem.Enums;
using UnityEngine;

namespace Core.WeaponSystem.Data
{
    [Serializable]
    public class WeaponData
    {
        [field:SerializeField] public float Cooldown { get; private set; }
    }
    
    [Serializable]
    public class ExtendableSpearWeaponData :WeaponData
    {
        [field:SerializeField] public float AttackTime { get; private set; }
        [field:SerializeField] public float Radius { get; private set; }
    }
}
