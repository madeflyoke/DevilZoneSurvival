using System;
using Core.Loot.Interfaces;
using UnityEngine;

namespace Core.Loot.Magnet
{
    public interface IMagnetableLoot
    {
        public Transform SelfTransform { get; }
        public void CallOnMagneted(IItemsLootOwner owner);
        public void CallOnStartMagneting();
    }
}
