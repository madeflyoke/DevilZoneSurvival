using System;

namespace Core.Scripts.Units.Components
{
    public abstract class UnitAbstractComponent<T> : UnitComponentBase where T : CData
    {
        public T Data;
    }
    
    [Serializable]
    public class CData { }
}