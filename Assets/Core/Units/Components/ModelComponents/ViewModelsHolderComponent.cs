using System.Collections.Generic;
using System.Linq;
using Core.Actions.Interfaces;
using Core.Loot.Interfaces;
using Core.Units.Components.Base;
using Core.ViewModelData.Interfaces;
using UnityEngine;

namespace Core.Units.Components.ModelComponents
{
    public abstract class ViewModelsHolderComponent : UnitAbstractComponent, IActionReceiversOwner
    {
        protected readonly List<IViewModel> ViewModels = new List<IViewModel>();
        
        public bool TryGetActionReceiver<T>(out T result) where T : IActionReceiver
        {
            result = (T)ViewModels.FirstOrDefault(x=>x.GetType()==typeof(T));
            if (result==null)
            {
                Debug.LogWarning($"No action receiver of type {typeof(T)}");
                return false;
            }
            return true;
        }
    }
}
