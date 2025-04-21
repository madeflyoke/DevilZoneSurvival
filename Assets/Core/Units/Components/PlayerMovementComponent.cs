using System;
using Core.Enums;
using Core.Units.Components.Base;
using Core.Utils;
using UnityEngine;

namespace Core.Units.Components
{
    [Serializable]
    public class PlayerMovementData : CData
    {
        public float Speed;
    }
    
    [ComponentName("PlayerMovementComponent")]
    [Serializable]
    public class PlayerMovementComponent : UnitAbstractDataComponent<PlayerMovementData>
    {
        private Transform _unitTransform;
        private Transform _unitRotationTransform;

        protected override void Construct()
        {
            base.Construct();
            _unitTransform = _context.Brain.GetUnitComponent<ViewHolderComponent>().Data.UnitT;
            _unitRotationTransform = _context.Brain.GetUnitComponent<ViewHolderComponent>().Data.UnitR;
        }

        public override void Execute()
        {
            var directions = GetDirection();
            
            var pos = _unitTransform.position;
    
            if (directions == 0)
            {
                _unitTransform.position = pos;

                return;
            }
            
            var direction = GetDirectionVec(directions);
            pos += Time.deltaTime * Data.Speed * direction;
            
            if (direction != Vector3.zero)
            {
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _unitRotationTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
    
            _unitTransform.position = GameplaySceneContext.Instance.Field.ClampedMoveInFieldZone(pos, _unitTransform.localScale);
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