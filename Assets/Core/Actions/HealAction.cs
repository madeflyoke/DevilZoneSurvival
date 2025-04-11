using Core.Actions.Interfaces;
using Core.Loot.Interfaces;

namespace Core.Actions
{
    public class HealAction : IAction
    {
        private int _healAmount;
        //healReciever
        public void Initialize(int healAmount) //healReciever
        {
            _healAmount = healAmount;   
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            //healReciever.Heal(_healAmount)
        }
     

        public void Dispose()
        {
            
        }
    }
}
