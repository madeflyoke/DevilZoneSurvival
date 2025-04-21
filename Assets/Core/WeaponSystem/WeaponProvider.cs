using Core.Units.Components;
using Core.Units.UnitBrains;
using Core.WeaponSystem.Data;
using Core.WeaponSystem.Enums;
using UnityEngine;

namespace Core.WeaponSystem
{
    public class WeaponProvider : MonoBehaviour
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;
        
        public void GetWeapon(UnitBrain target, WeaponType weaponType)
        {
            var component = target.GetUnitComponent<WeaponHolderComponent>();

            if (component == null)
            {
                return;
            }

            var weaponData = _weaponsConfig.GetWeaponConfigData(weaponType);
           var weaponPrefab = weaponData.Prefab;
            
            if (weaponPrefab == null)
            {
                return;
            }
            
            var weaponInstance = Instantiate(weaponPrefab, component.Data.WeaponContainer);
            weaponInstance.Initialize(target);
            
           component.Data.Weapons.Add(weaponInstance);
        }
    }
}