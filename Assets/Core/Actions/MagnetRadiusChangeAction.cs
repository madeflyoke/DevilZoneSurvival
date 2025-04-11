using System;
using Core.Actions.Interfaces;
using Core.Loot;
using Core.Loot.Interfaces;
using R3;

namespace Core.Actions
{
    public class MagnetRadiusChangeAction : IAction
    {
        private float _newRadius;
        private float _duration;

        private IDisposable _disposable;
        
        public MagnetRadiusChangeAction(float newRadius, float duration)
        {
            _newRadius = newRadius;
            _duration = duration;
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<LootCollector>(out var result))
            {
                result.SetCurrentRadius(_newRadius);
                _disposable = Observable.Timer(TimeSpan.FromSeconds(_duration))
                    .Subscribe(_ => result.SetDefaultRadius());
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
