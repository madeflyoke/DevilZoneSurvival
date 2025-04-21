using System;
using Core.Units.Interfaces;
using Core.Units.Models;
using UnityEngine;

namespace Core.Units.Components.Base
{
    [Serializable]
    public abstract class UnitComponentBase : IUnitComponent
    {
        [HideInInspector]
        public string EditorName;
        
        protected UnitContext _context;

        public void Initialize(UnitContext context)
        {
            _context = context;

            Construct();
        }
        
        public virtual void Execute() { }
        
        protected virtual void Construct() { } 
    }
}