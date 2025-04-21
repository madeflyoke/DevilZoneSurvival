using System.Collections.Generic;
using Core.Units.Components;
using Core.Units.Components.Base;
using Core.Units.Models;
using UnityEngine;

namespace Core.Units.UnitBrains
{
    public class UnitBrain : MonoBehaviour
    {
        [SerializeField] protected UnitContext UnitContext;
    
        [Space]
        [SerializeReference] public List<UnitComponentBase> Components = new();
        private bool _initialized;
        
        public virtual void Initialize()
        {
            UnitContext.Brain = this;
            foreach (var component in Components)
            {
                component.Initialize(UnitContext);
            }

            _initialized = true;
        }
        
        private void Update()
        {
            if (!_initialized)
                return;
            
            Components.ForEach(component => component.Execute());
        }

        public T GetUnitComponent<T>() where T : UnitComponentBase
        {
            return Components.Find(component => component is T) as T;
        }
    }
}