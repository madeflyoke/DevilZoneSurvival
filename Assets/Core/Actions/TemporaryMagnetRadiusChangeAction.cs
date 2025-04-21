using System;
using Core.Actions.Interfaces;
using Core.Loot;
using Core.Loot.Interfaces;
using Core.Stats.Enum;
using Core.Stats.ViewModel;
using Core.Units.Components;
using R3;

namespace Core.Actions
{
    public class TemporaryMagnetRadiusChangeAction : IAction
    {
        private int _appendPercent;
        private int _duration;

        private IDisposable _disposable;
        
        public TemporaryMagnetRadiusChangeAction(int appendPercent, int duration)
        {
            _appendPercent = appendPercent;
            _duration = duration;
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<StatsViewModel>(out var result))
            {
                result.OnIncreasePercent(StatType.CURRENT_MAGNET, _appendPercent);
                
                _disposable = Observable.Timer(TimeSpan.FromSeconds(_duration))
                    .Subscribe(_ => result.OnRevertPercent(StatType.CURRENT_MAGNET,_appendPercent));
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
