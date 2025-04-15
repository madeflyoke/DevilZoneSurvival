using DG.Tweening;
using UnityEngine;

namespace Core.WeaponSystem.Implementations
{
    public class MeleeSword : Weapon
    {
        [SerializeField] private float _endPosition;

        protected override void ViewAttack()
        {
            var endPosition = _weaponT.localPosition + _unitContext.UnitR.right * _endPosition;
            _weaponT.rotation = _unitContext.UnitR.rotation;
            
            _attackSequence = DOTween.Sequence()
                .Append(_weaponT.DOLocalMove(endPosition, _attackSpeed * 0.9f))
                .Append(_weaponT.DOLocalMove(Vector3.zero, _attackSpeed * 0.1f))
                .OnComplete(_cooldownTimer.Restart)
                .Play();
        }
    }
}