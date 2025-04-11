using System;
using Core.Scripts.Units.Interfaces;
using Core.Scripts.Units.Models;
using UnityEngine;

namespace Core.Scripts.Units.Components
{
    [Serializable]
    public abstract class UnitComponentBase : IUnitComponent
    {
        [SerializeField] protected UnitContext _context;

        public void Initialize(UnitContext context)
        {
            _context = context;
        }
    
        public virtual void Execute() { }
    }
}