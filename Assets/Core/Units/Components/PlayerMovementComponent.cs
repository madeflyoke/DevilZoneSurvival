using System;
using Core.Scripts.Enums;
using Core.Scripts.Utils;
using UnityEngine;

namespace Core.Scripts.Units.Components
{
    [ComponentName("Player Movement")]
    [Serializable]
    public class PlayerMovementComponent : UnitComponentBase
    {
        [Serializable]
        public class Context
        {
            public float MovementSpeed;
        }
        
        [SerializeField] private Context _pmContext;
        
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
            
            var direction = Vector3.zero;
    
            if (directions.HasFlag(MovementDirection.Up))
            {
                direction = Vector3.up;
            }
            if (directions.HasFlag(MovementDirection.Down))
            {
                direction = Vector3.down;
            }
            if (directions.HasFlag(MovementDirection.Left))
            {
                direction = Vector3.left;
            }
            if (directions.HasFlag(MovementDirection.Right))
            {
                direction = Vector3.right;
            }
            
            pos += Time.deltaTime * _pmContext.MovementSpeed * direction;
            
            if (direction != Vector3.zero)
            {
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _context.UnitT.rotation = Quaternion.Euler(0, 0, angle);
            }
    
            return MonoContext.Instance.Field.ClampedMoveInFieldZone(pos, _context.UnitT.localScale);
        }
    }
}