using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Loot.Interfaces;
using Core.Scripts.Utils;
using Core.Services;
using Core.Services.Pause.Interfaces;
using Core.Stats.Enum;
using Core.Stats.ViewModel;
using R3;
using UnityEditor;
using UnityEngine;

namespace Core.Loot
{
    public class LootCollector : MonoBehaviour, IPausable
    {
        public bool IsPaused { get; private set; }
        
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _magnetSpeed;
        [SerializeField] private float _magnetAccelerationSpeed;
        
        private IActionReceiversOwner _actionReceiversOwner;

        private readonly List<ICollectableLoot> _currentCollectables = new List<ICollectableLoot>();
        private readonly Dictionary<ICollectableLoot, float> _currentCollectablesAccelerators = new Dictionary<ICollectableLoot, float>();
        private float _currentRadius;

        private StatsViewModel _statsViewModel;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Initialize(IActionReceiversOwner actionReceiversOwner, StatsViewModel statsViewModel) //maybe take from service by own
        {
            _statsViewModel = statsViewModel;
            ServiceLocator.Instance.PauseService.Register(this);
            _actionReceiversOwner = actionReceiversOwner;
            BindStat();
        }

        private void BindStat()
        {
            _statsViewModel.GetRelatedBind(StatType.CURRENT_MAGNET)
                .Subscribe(x=>SetCurrentRadius(x)).AddTo(_disposable);
        }

        private void UnbindStat()
        {
            _disposable?.Dispose();
        }

        public void IncreaseCurrentRadius(float appendRadius)
        {
            SetCurrentRadius(_currentRadius+appendRadius);
        }
        
        public void SetCurrentRadius(float value)
        {
            _currentRadius = value;
            _collider.radius = _currentRadius;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Constants.Layers.Loot)
            {
                var collectable =other.GetComponent<ICollectableLoot>();
                if (_currentCollectables.Contains(collectable)==false)
                {
                    _currentCollectables.Add(collectable);
                    _currentCollectablesAccelerators.Add(collectable, 1);
                    collectable.CallOnStartCollecting();
                }
            }
        }

        private void Update()
        {
            if (IsPaused)
            {
                return;
            }
            
            for (int i = _currentCollectables.Count-1; i >= 0; i--)
            {
                var collectable = _currentCollectables[i];
                
                var dir = transform.position - collectable.SelfTransform.position;
                if (dir.sqrMagnitude <= 0.05f)
                {
                    _currentCollectables.Remove(collectable);
                    _currentCollectablesAccelerators.Remove(collectable);
                    
                    collectable.CallOnCollected(_actionReceiversOwner);
                    continue;
                }
                
                var acceleration = _currentCollectablesAccelerators[collectable];
                acceleration += _magnetAccelerationSpeed;
                _currentCollectablesAccelerators[collectable] = acceleration;

                var finalSpeed = Mathf.Pow(_magnetSpeed, 1 + acceleration);
                
                collectable.SelfTransform.position += dir.normalized * (finalSpeed * Time.deltaTime);
            }
        }
        
        public void SetPause(bool value)
        {
            IsPaused = value;
        }
    }
}
