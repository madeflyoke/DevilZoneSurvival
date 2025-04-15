using System;
using System.Collections.Generic;
using Core.Scripts.Utils;
using Core.WeaponSystem;
using UnityEngine;

namespace Core.Scripts.Units.Components
{
    [Serializable]
    public class WeaponCData : CData
    {
        public List<Weapon> Weapons;
        public Transform WeaponContainer;
    }
    
    [ComponentName("Weapon Holder")]
    [Serializable]
    public class WeaponHolderComponent : UnitAbstractComponent<WeaponCData>
    {
        public override void Execute()
        {
            Data.Weapons.ForEach(w => w.Attack());
        }
    }
}