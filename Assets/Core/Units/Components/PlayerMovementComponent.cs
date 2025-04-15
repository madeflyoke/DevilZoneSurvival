using System;
using Core.Scripts.Enums;
using Core.Scripts.Utils;
using UnityEngine;

namespace Core.Scripts.Units.Components
{
    [Serializable]
    public class PlayerMovementData : CData
    {
        public float Speed;
    }
    
    [ComponentName("Player Movement")]
    [Serializable]
    public class PlayerMovementComponent : UnitAbstractComponent<PlayerMovementData>
    {
        public override void Execute()
        {
            var directions = GetDirection();
            
            var pos = _context.UnitT.position;
    
            if (directions == 0)
            {
                _context.UnitT.position = pos;

                return;
            }
            
            var direction = GetDirectionVec(directions);
            pos += Time.deltaTime * Data.Speed * direction;
            
            if (direction != Vector3.zero)
            {
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _context.UnitR.rotation = Quaternion.Euler(0f, 0f, angle);
            }
    
            _context.UnitT.position = MonoContext.Instance.Field.ClampedMoveInFieldZone(pos, _context.UnitT.localScale);
        }

        private MovementDirection GetDirection()
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

            return directions;
        }

        private Vector3 GetDirectionVec(MovementDirection directions)
        {
            var direction = Vector3.zero;

            if (directions.HasFlag(MovementDirection.Up))
            {
                direction += Vector3.up;
            }
            if (directions.HasFlag(MovementDirection.Down))
            {
                direction += Vector3.down;
            }
            if (directions.HasFlag(MovementDirection.Left))
            {
                direction += Vector3.left;
            }
            if (directions.HasFlag(MovementDirection.Right))
            {
                direction += Vector3.right;
            }
            
            return direction;
        }
    }
}