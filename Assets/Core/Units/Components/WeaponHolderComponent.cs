using System;
using System.Collections.Generic;
using Core.Units.Components.Base;
using Core.Utils;
using Core.WeaponSystem;
using UnityEngine;

namespace Core.Units.Components
{
    [Serializable]
    public class WeaponHolderCData : CData
    {
        public List<Weapon> Weapons;
        public Transform WeaponContainer;
    }
    
    [ComponentName("WeaponHolderComponent")]
    [Serializable]
    public class WeaponHolderComponent : UnitAbstractDataComponent<WeaponHolderCData>
    {
        public override void Execute()
        {
            Data.Weapons.ForEach(w => w.Attack());
        }
    }
}