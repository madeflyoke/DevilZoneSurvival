using System;
using UnityEngine;

namespace Core.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }
        
        public ItemsService ItemsService { get; private set; }
        
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            ItemsService = new ItemsService();
            Instance = this;
        }
    }
}
