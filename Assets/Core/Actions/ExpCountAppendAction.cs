using Core.Actions.Interfaces;
using Core.Loot.Interfaces;
using Core.Progress.ViewModel;
using Core.Units.Components;

namespace Core.Actions
{
    public class ExpCountAppendAction : IAction
    {
        private readonly int _additionalCount;
        
        public ExpCountAppendAction(int additionalCount)
        {
            _additionalCount = additionalCount;
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<LevelViewModel>(out var result))
            {
                result.OnExpIncreased(_additionalCount);
            }
        }
        
        public void Dispose()
        {
            
        }
    
    }
}
