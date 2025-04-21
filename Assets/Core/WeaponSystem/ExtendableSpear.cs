using Core.Units.UnitBrains;
using Core.WeaponSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace Core.WeaponSystem
{
    public class ExtendableSpear: Weapon
    {
        [SerializeField] private ExtendableSpearWeaponData _weaponData;
        
        protected override WeaponData BaseWeaponData => _weaponData;

        public override void Initialize(UnitBrain unitBrain)
        {
            base.Initialize(unitBrain);
        }

        protected override void ViewAttack()
        {
            var endPosition = _weaponT.localPosition + _unitRotationTransform.right * _weaponData.Radius;
            _weaponT.rotation = _unitRotationTransform.rotation;
            
            _attackSequence = DOTween.Sequence()
                .Append(_weaponT.DOLocalMove(endPosition, _weaponData.AttackTime * 0.9f))
                .Append(_weaponT.DOLocalMove(Vector3.zero, _weaponData.AttackTime * 0.1f))
                .OnComplete(_cooldownTimer.Restart)
                .SetUpdate(UpdateType.Fixed)
                .Play();
        }
    }
}