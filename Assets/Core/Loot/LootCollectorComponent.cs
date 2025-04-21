using System;
using System.Collections.Generic;
using Core.Loot.Interfaces;
using Core.Services;
using Core.Services.Pause.Interfaces;
using Core.Stats.Enum;
using Core.Stats.ViewModel;
using Core.Units.Components;
using Core.Units.Components.Base;
using Core.Units.Components.ModelComponents;
using Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Core.Loot
{
    [Serializable]
    public class LootCollectorCData : CData
    {
        public CircleCollider2D Collider;
        public float MagnetSpeed;
        public float MagnetAccelerationSpeed;
    }
    
    [ComponentName("LootCollectorComponent")]
    public class LootCollectorComponent : UnitAbstractDataComponent<LootCollectorCData>, IPausable
    {
        public bool IsPaused { get; private set; }
        
        private float _currentRadius;

        private IActionReceiversOwner _actionReceiversOwner;
        private readonly List<ICollectableLoot> _currentCollectables = new List<ICollectableLoot>();
        private readonly Dictionary<ICollectableLoot, float> _currentCollectablesAccelerators = new Dictionary<ICollectableLoot, float>();

        private StatsViewModel _statsViewModel;
        private CompositeDisposable _bindDisposable = new CompositeDisposable();
        private IDisposable _onTriggerObservable;
        
        private Transform _unitTransform;

        protected override void Construct()
        {
            ServiceLocator.Instance.PauseService.Register(this);

            _unitTransform = _context.Brain.GetUnitComponent<ViewHolderComponent>().Data.UnitT;
            
            _actionReceiversOwner = _context.Brain.GetUnitComponent<ViewModelsHolderComponent>();
            _actionReceiversOwner.TryGetActionReceiver(out _statsViewModel);

            BindStat();

            _onTriggerObservable = Data.Collider.OnTriggerEnter2DAsObservable().Subscribe(OnTriggerEnterManual);
        }

        private void BindStat()
        {
            _statsViewModel.GetRelatedBind(StatType.CURRENT_MAGNET)
                .Subscribe(SetCurrentRadius).AddTo(_bindDisposable);
        }

        private void UnbindStat()
        {
            _bindDisposable?.Dispose();
        }

        public override void Execute()
        {
            if (IsPaused)
            {
                return;
            }
            
            for (int i = _currentCollectables.Count-1; i >= 0; i--)
            {
                var collectable = _currentCollectables[i];
                
                var dir = _unitTransform.position - collectable.SelfTransform.position;
                if (dir.sqrMagnitude <= 0.05f)
                {
                    _currentCollectables.Remove(collectable);
                    _currentCollectablesAccelerators.Remove(collectable);
                    
                    collectable.CallOnCollected(_actionReceiversOwner);
                    continue;
                }
                
                var acceleration = _currentCollectablesAccelerators[collectable];
                acceleration += Data.MagnetAccelerationSpeed;
                _currentCollectablesAccelerators[collectable] = acceleration;

                var finalSpeed = Mathf.Pow(Data.MagnetSpeed, 1 + acceleration);
                
                collectable.SelfTransform.position += dir.normalized * (finalSpeed * Time.deltaTime);
            }
        }
        
        public void IncreaseCurrentRadius(float appendRadius)
        {
            SetCurrentRadius(_currentRadius+appendRadius);
        }
        
        private void SetCurrentRadius(float value)
        {
            _currentRadius = value;
            Data.Collider.radius = _currentRadius;
        }
        
        private void OnTriggerEnterManual(Collider2D other)
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
        
        public void SetPause(bool value)
        {
            IsPaused = value;
            Data.Collider.enabled = !value;
        }
    }
}
