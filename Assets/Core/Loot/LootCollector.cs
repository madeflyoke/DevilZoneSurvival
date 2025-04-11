using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Loot.Interfaces;
using Core.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Core.Loot
{
    public class LootCollector : MonoBehaviour, IActionReceiver
    {
        [SerializeField] private float _radius;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _magnetSpeed;
        [SerializeField] private float _magnetAccelerationSpeed;
        
        private IActionReceiversOwner _actionReceiversOwner;

        private readonly List<ICollectableLoot> _currentCollectables = new List<ICollectableLoot>();
        private readonly Dictionary<ICollectableLoot, float> _currentCollectablesAccelerators = new Dictionary<ICollectableLoot, float>();
        private float _currentRadius;

        public void Initialize(IActionReceiversOwner actionReceiversOwner)
        {
            _actionReceiversOwner = actionReceiversOwner;
            SetDefaultRadius();
        }

        public void SetCurrentRadius(float radius)
        {
            _currentRadius = radius;
            _collider.radius = _currentRadius;
        }

        public void SetDefaultRadius()
        {
            SetCurrentRadius(_radius);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Constants.Layers.Loot)
            {
                var collectable =other.GetComponent<ICollectableLoot>();
                if (_currentCollectables.Contains(collectable)==false)
                {
                    _currentCollectables.Add(collectable);
                    _currentCollectablesAccelerators.Add(collectable, 1);
                    collectable.CallOnStartCollecting();
                }
            }
        }

        private void Update()
        {
            for (int i = _currentCollectables.Count-1; i >= 0; i--)
            {
                var collectable = _currentCollectables[i];
                
                var dir = transform.position - collectable.SelfTransform.position;
                if (dir.sqrMagnitude <= 0.05f)
                {
                    _currentCollectables.Remove(collectable);
                    _currentCollectablesAccelerators.Remove(collectable);
                    
                    collectable.CallOnCollected(_actionReceiversOwner);
                    continue;
                }
                
                var acceleration = _currentCollectablesAccelerators[collectable];
                acceleration += _magnetAccelerationSpeed;
                _currentCollectablesAccelerators[collectable] = acceleration;

                var finalSpeed = Mathf.Pow(_magnetSpeed, 1 + acceleration);
                
                collectable.SelfTransform.position += dir.normalized * (finalSpeed * Time.deltaTime);
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = _radius;
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.back,  _radius);
        }

#endif
       
    }
}
