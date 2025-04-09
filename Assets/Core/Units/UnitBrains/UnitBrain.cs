using System.Collections.Generic;
using Core.Scripts.Units.Components;
using Core.Scripts.Units.Models;
using UnityEngine;

namespace Core.Units.UnitBrains
{
    public class UnitBrain : MonoBehaviour
    {
        [SerializeField] protected UnitContext _unitContext;
    
        [Space]
        [SerializeReference] public List<UnitComponentBase> Components = new();
    
        private void Awake()
        {
            foreach (var component in Components)
            {
                component.Initialize(_unitContext);
            }
        }
        
        private void Update()
        {
            Components.ForEach(component => component.Execute());
        }
    }
}