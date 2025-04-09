using System;

namespace Core.Scripts.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentNameAttribute : Attribute
    {
        public readonly string Name;
        public ComponentNameAttribute(string name) => Name = name;
    }
}