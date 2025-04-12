using System;
using Core.Actions.Enum;

namespace Core.Actions.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionTypeAttribute : System.Attribute
    {
        public ActionType ActionType;
        public ActionTypeAttribute(ActionType actionType)=>ActionType = actionType;
    }
}
