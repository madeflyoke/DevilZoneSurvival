using System.Collections.Generic;
using Core.Scripts.Units.Components;
using Core.Scripts.Units.Models;
using UnityEngine;

namespace Core.Units.UnitBrains
{
    public class UnitBrain : MonoBehaviour
    {
        public UnitContext UnitContext;
    
        [Space]
        [SerializeReference] public List<UnitComponentBase> Components = new();
    
        private void Awake()
        {
            foreach (var component in Components)
            {
                component.Initialize(UnitContext);
            }
        }
        
        private void Update()
        {
            Components.ForEach(component => component.Execute());
        }

        public T GetUnitComponent<T>() where T : UnitComponentBase
        {
            return Components.Find(component => component.GetType() == typeof(T)) as T;
        }
    }
}