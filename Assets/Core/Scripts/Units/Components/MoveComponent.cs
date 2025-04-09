using System;
using Core.Scripts.Enums;
using UnityEngine;

namespace Core.Scripts.Units.Components
{
    [Serializable]
    public class MoveComponent : UnitComponentBase
    {
        [Serializable]
        public class MovementContext
        {
            public float MovementSpeed;
        }
        
        [SerializeField] private MovementContext _movementContext;
    
        public override void Execute()
        {
            var directions = MovementDirection.None;
            
            if (Input.GetKey(KeyCode.W))
            {
                directions |= MovementDirection.Up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                directions |= MovementDirection.Down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                directions |= MovementDirection.Left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                directions |= MovementDirection.Right;
            }
    
            _context.UnitT.position = GetNewPosition(directions);
        }
    
        public Vector3 GetNewPosition(MovementDirection directions)
        {
            var pos = _context.UnitT.position;
    
            if (directions == 0)
            {
                return pos;
            }
            
            var step = Time.deltaTime * _movementContext.MovementSpeed;
    
            if (directions.HasFlag(MovementDirection.Up))
            {
                pos += Vector3.up * step;
            }
            if (directions.HasFlag(MovementDirection.Down))
            {
                pos += Vector3.down * step;
            }
            if (directions.HasFlag(MovementDirection.Left))
            {
                pos += Vector3.left * step;
            }
            if (directions.HasFlag(MovementDirection.Right))
            {
                pos += Vector3.right * step;
            }
    
            return MonoContext.Instance.Field.ClampedMoveInFieldZone(pos, _context.UnitT.localScale);
        }
    }
}