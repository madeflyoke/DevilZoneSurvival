using Core.Scripts.Units.Models;
using Core.WeaponSystem.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Core.WeaponSystem
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [Header("References")]
        [SerializeField] protected Transform _weaponT;
        
        [Header("Configuration")]
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected float _cooldown;
        
        protected CooldownTimer _cooldownTimer;
        protected Sequence _attackSequence;
        protected UnitContext _unitContext;
        
        private bool _initialized;

        public void Initialize(UnitContext unitContext)
        {
            _initialized = true;
            _unitContext = unitContext;
            
            _cooldownTimer = new(_cooldown);
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