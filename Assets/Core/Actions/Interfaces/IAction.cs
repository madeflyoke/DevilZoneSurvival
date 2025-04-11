using System;
using Core.Loot.Interfaces;

namespace Core.Actions.Interfaces
{
    public interface IAction : IDisposable
    {
        public void TryExecute(IActionReceiversOwner owner);
    }
}
