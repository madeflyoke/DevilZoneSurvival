using System;
using Core.Units.UnitBrains;
using UnityEngine;

namespace Core.Units.Models
{
    [Serializable]
    public class UnitContext
    {
        [HideInInspector] public UnitBrain Brain;
    }
}