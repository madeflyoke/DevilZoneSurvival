using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Units.Components;
using Core.Units.UnitBrains;
using Core.WeaponSystem.Interfaces;
using UnityEngine;

namespace Core.WeaponSystem
{
    public class WeaponGiver : MonoBehaviour
    {
        [SerializeField] private UnitBrain _targetBrain;
        [SerializeField] private List<Weapon> _weaponPool;

        public void GiveWeapon<T>() where T : IWeapon
        {
            var component = _targetBrain.GetUnitComponent<WeaponHolderComponent>();

            if (component == null)
            {
                return;
            }
            
            var weaponPrefab = _weaponPool.FirstOrDefault(w => w.GetType() == typeof(T));

            if (weaponPrefab == null)
            {
                return;
            }

            var weaponInstance = Instantiate(weaponPrefab, component.Data.WeaponContainer);
            weaponInstance.Initialize(_targetBrain.UnitContext);
            
            component.Data.Weapons.Add(weaponInstance);
        }
    }
}