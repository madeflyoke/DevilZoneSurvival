using System;
using Core.Actions.Attribute;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Loot;
using Core.Loot.Interfaces;
using R3;

namespace Core.Actions
{
    [ActionType(ActionType.STAT_CHANGED)]
    public class MagnetRadiusChangeAction : IAction, IBuffAction
    {
        private float _appendRadius;
        private int _duration;

        private IDisposable _disposable;

        public MagnetRadiusChangeAction(float appendRadius)
        {
            _appendRadius = appendRadius;
            _duration = -1;
        }
        
        public MagnetRadiusChangeAction(float appendRadius, int duration)
        {
            _appendRadius = appendRadius;
            _duration = duration;
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<LootCollector>(out var result))
            {
                result.IncreaseCurrentRadius(_appendRadius);
                if (_duration==1)
                {
                    return;
                }
                _disposable = Observable.Timer(TimeSpan.FromSeconds(_duration))
                    .Subscribe(_ => result.SetDefaultRadius());
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public string FormatDescription(string sourceDescription)
        {
            return string.Format(sourceDescription, _appendRadius);
        }
    }
}
