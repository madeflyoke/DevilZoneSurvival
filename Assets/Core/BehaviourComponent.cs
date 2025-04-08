using System;
using R3;
using UnityEngine;

namespace Core
{
    public abstract class BehaviourComponent : MonoBehaviour
    {
        public event Action AwakeEvent;
        public event Action StartEvent;
        public event Action EnableEvent;
        public event Action DisableEvent;
        public event Action DestroyEvent;

        public bool IsDestroyed { get; private set; }
        public Transform Transform { get; private set; }

        private int _defaultUpdate;
        private int _reactUpdate;
        private CompositeDisposable _disposable = new CompositeDisposable();

        protected virtual void Awake()
        {
            Transform = GetComponent<Transform>();
            
            Observable
                .EveryUpdate(UnityFrameProvider.Update)
                .Subscribe(_ => { Tick(); })
                .AddTo(_disposable);
            Observable
                .EveryUpdate(UnityFrameProvider.FixedUpdate)
                .Subscribe(_ => { FixedTick(); })
                .AddTo(_disposable);
            Observable
                .EveryUpdate(UnityFrameProvider.PostFixedUpdate)
                .Subscribe(_ => { LateTick(); })
                .AddTo(_disposable);
            AwakeEvent?.Invoke();
            
        }
        protected virtual void Start() => StartEvent?.Invoke();
        protected virtual void OnEnable() => EnableEvent?.Invoke();
        protected virtual void OnDisable() => DisableEvent?.Invoke();
        
        protected virtual void Tick() { }
        protected virtual void FixedTick() { }
        protected virtual void LateTick() { }
        
        protected virtual void OnDestroy()
        {
            _disposable?.Dispose();
            Transform = null;
            IsDestroyed = true;
            DestroyEvent?.Invoke();
        }
    }
}