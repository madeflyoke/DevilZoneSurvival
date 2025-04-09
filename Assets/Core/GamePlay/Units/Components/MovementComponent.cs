using UnityEngine;

namespace Core.GamePlay.Units.Components
{
   public class MovementComponent : BehaviourComponent
   {
      private const float MIN_DISTINATION_DISTANCE = 0.2f;
     
      private float _speed;
      private MovementType _movementType = MovementType.NONE;
      private bool _isMoving;
      private Vector3? _destination;
      private Transform _target;
      
      public float DistanceToDestination { get; private set; }
      
      public void Stop() =>_isMoving = false;

      public void Continue() => _isMoving = true;
      
      public void Setup(float speed)
      {
         _speed = speed;
      }
      
      public void SetDestination(Vector3 destination)
      {
         _movementType = MovementType.MOVE_TO_POINT;
         _destination = destination;
         Move();
      }
      
      public void SetDestination(Transform target)
      {
         _movementType = MovementType.FOLLOW_TRANSFORM;
         _target = target;
         Move();
      }
      
      protected override void Tick()
      {
         base.Tick();
         
         if(DistanceToDestination <= MIN_DISTINATION_DISTANCE) return;
         Move();
         CalculateDistanceToDestination();
      }
      
      private void Move()
      {
         if(DistanceToDestination <= MIN_DISTINATION_DISTANCE || _movementType== MovementType.NONE) return;
      }
      
      private void CalculateDistanceToDestination()
      {
         switch (_movementType)
         {
            case MovementType.NONE:DistanceToDestination=0;break;
            case MovementType.MOVE_TO_POINT:
            {
               DistanceToDestination = Vector3.Distance(_destination.Value, Transform.position);
               break;
            }
            case MovementType.FOLLOW_TRANSFORM:
            {
               DistanceToDestination = Vector3.Distance(_target.position, Transform.position);
               break;
            }
         }
      }

      private enum MovementType
      {
         NONE,
         FOLLOW_TRANSFORM,
         MOVE_TO_POINT
      }
   }
}
