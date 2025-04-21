using Core.GamePlay.Units.Components;
using Core.GamePlay.Units.Configs;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Core.GamePlay.Units
{
    public abstract class DefaultEnemyUnit<T>: BehaviourComponent where T : EnemyBaseStatsConfig
    {
        [SerializeField] protected HealthComponent _healthComponent;
        [SerializeField] protected MovementComponent _movementComponent;
        
        protected T _setup;
        protected GameObject _target;
        protected State _currentState;
        protected bool _active;
        
        public HealthComponent HealthComponent => _healthComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public void SetTarget(GameObject target) => _target = target;

        public virtual void Setup(T setup)
        {
            _currentState = State.Idle;
            _setup = setup;
            _healthComponent.Setup(_setup.health);
            _movementComponent.Setup(_setup.speed);
        }

        public virtual void Enable()
        {
            _active = true;
            _movementComponent.SetDestination(_target.transform);
            _movementComponent.Continue();
            _currentState = State.Moving;
        }

        public virtual void Disable()
        {
            _active = false;
            _movementComponent.Stop();
        }

        protected override void LateTick()
        {
            base.LateTick();
            if(_active==false || _currentState!=State.Moving) return;
            if (_movementComponent.DistanceToDestination <= _setup.attackRange)
            {
                _currentState = State.Attacking;
                Attack().Forget();
            }
        }

        protected virtual async UniTaskVoid Attack()
        {
        }
        
        protected enum State
        {
            Idle,
            Moving,
            Attacking,
        }
    }
}