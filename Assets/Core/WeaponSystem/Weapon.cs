using Core.Units.Components;
using Core.Units.Models;
using Core.Units.UnitBrains;
using Core.Utils;
using Core.WeaponSystem.Data;
using Core.WeaponSystem.Enums;
using Core.WeaponSystem.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Core.WeaponSystem
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        protected abstract WeaponData BaseWeaponData { get; }
        
        [field:SerializeField] public WeaponType WeaponType { get; private set; } 
        
        [SerializeField] protected Transform _weaponT;
        
        protected CooldownTimer _cooldownTimer;
        protected Sequence _attackSequence;
        
        protected UnitBrain _unitBrain;
        protected Transform _unitRotationTransform;
        
        private bool _initialized;

        public virtual void Initialize(UnitBrain unitBrain)
        {
            _initialized = true;
            _unitBrain = unitBrain;
            _unitRotationTransform = _unitBrain.GetUnitComponent<ViewHolderComponent>().Data.UnitR;
            
            _cooldownTimer = new(BaseWeaponData.Cooldown);
            _cooldownTimer.Start();
        }

        public void Attack()
        {
            if (!_initialized)
            {
                return;
            }
            
            _cooldownTimer.Tick(Time.deltaTime);

            if (_cooldownTimer.IsFinished && !_attackSequence.IsActive())
            {
                ViewAttack();
            }
        }

        protected abstract void ViewAttack();
    }
}