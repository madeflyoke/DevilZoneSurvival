using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Loot.Data;

namespace Core.Loot.Interfaces
{
    public interface IActionReceiversOwner
    {
        public bool TryGetActionReceiver<T>(out T result) where T : IActionReceiver;
    }
}
