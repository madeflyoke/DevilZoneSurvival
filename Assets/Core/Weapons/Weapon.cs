using DG.Tweening;
using UnityEngine;

namespace Core.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _weaponT;
        [SerializeField] private Transform _unitT;
        [SerializeField] private float _endPosition;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _cooldown;
        
        private CooldownTimer _cooldownTimer;
        private Sequence _attackSequence;

        private void Awake()
        {
            _cooldownTimer = new CooldownTimer(_cooldown);
        }

        private void Start()
        {
            _cooldownTimer.Start();
        }

        private void Update()
        {
            _cooldownTimer.Tick(Time.deltaTime);

            if (_cooldownTimer.IsFinished && !_attackSequence.IsActive())
            {
                Attack();
            }
        }

        private void Attack()
        {
            var endPosition = _weaponT.localPosition + _unitT.right * _endPosition;
            
            _attackSequence = DOTween.Sequence()
                .Append(_weaponT.DOLocalMove(endPosition, _attackSpeed * 0.9f))
                .Append(_weaponT.DOLocalMove(Vector3.zero, _attackSpeed * 0.1f))
                .OnComplete(_cooldownTimer.Restart)
                .Play();
        }
    }
}