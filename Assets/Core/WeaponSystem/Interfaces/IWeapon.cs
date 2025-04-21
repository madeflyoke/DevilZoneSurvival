using Core.WeaponSystem.Enums;

namespace Core.WeaponSystem.Interfaces
{
    public interface IWeapon
    {
        public WeaponType WeaponType { get; }
        void Attack();
    }
}