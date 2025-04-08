using Core.GamePlay.Units.Components;
using UnityEngine;

namespace Core.GamePlay.Units
{
    public abstract class DefaultEnemyUnit : BehaviourComponent
    {
        [SerializeField] protected HealthComponent _healthComponent;
        [SerializeField] protected MovementComponent _movementComponent;
        private GameObject _target;

        public HealthComponent HealthComponent => _healthComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public void SetTarget(GameObject target) => _target = target;

        protected virtual void Attack()
        {
        }
    }
}