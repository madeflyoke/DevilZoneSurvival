using System;

namespace Core.Enums
{
    [Flags]
    public enum MovementDirection
    {
        None = 0,
        Up = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4,
    }
}