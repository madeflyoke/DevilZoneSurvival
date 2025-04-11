using System;
using System.Collections.Generic;
using Core.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Core.Loot.Magnet
{
    public class LootMagnetOwner : MonoBehaviour
    {
        [SerializeField] private PlayerItemsLootOwner _lootOwner; //can be IItemsLootOwner but cant serialize
        [SerializeField] private float _radius;
        [SerializeField] private float _magnetSpeed;
        
        private readonly List<IMagnetableLoot> _currentMagnetables = new List<IMagnetableLoot>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Constants.Layers.Loot)
            {
                var magnetable =other.GetComponent<IMagnetableLoot>();
                if (_currentMagnetables.Contains(magnetable)==false)
                {
                    _currentMagnetables.Add(magnetable);
                    magnetable.CallOnStartMagneting();
                }
            }
        }

        private void Update()
        {
            for (int i = _currentMagnetables.Count-1; i >= 0; i--)
            {
                var magnetable = _currentMagnetables[i];
                
                var dir = transform.position - magnetable.SelfTransform.position;
                if (dir.magnitude <= 0.1f)
                {
                    _currentMagnetables.Remove(magnetable);
                    magnetable.CallOnMagneted(_lootOwner);
                    continue;
                }
                magnetable.SelfTransform.position += dir.normalized * (_magnetSpeed * Time.deltaTime);
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
