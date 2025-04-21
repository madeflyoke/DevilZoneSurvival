using System;

namespace Core.Units.Components.Base
{
    public abstract class UnitAbstractDataComponent<T> : UnitComponentBase where T : CData
    {
        public T Data;
    }

    [Serializable]
    public class CData { }
}