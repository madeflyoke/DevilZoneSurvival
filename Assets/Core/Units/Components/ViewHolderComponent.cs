using System;
using Core.Units.Components.Base;
using Core.Utils;
using UnityEngine;

namespace Core.Units.Components
{
    [Serializable]
    public class ViewHolderData : CData
    {
        public Transform UnitT;
        public Transform UnitR;
    }
    
    [ComponentName("ViewHolderComponent")]
    public class ViewHolderComponent : UnitAbstractDataComponent<ViewHolderData>
    {
        
    }
}
