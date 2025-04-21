using Core.Actions.Interfaces;
using Core.Units.Components;

namespace Core.Loot.Interfaces
{
    public interface IActionReceiversOwner
    {
        public bool TryGetActionReceiver<T>(out T result) where T : IActionReceiver;
    }
}
