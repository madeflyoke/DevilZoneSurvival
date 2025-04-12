using System;
using Core.Services.Pause;
using EasyButtons;
using UnityEngine;

namespace Core.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }
        
        public ItemsService ItemsService { get; private set; }
        public ProgressService ProgressService { get; private set; }
        public PauseService PauseService { get; private set; }
        
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            PauseService = new PauseService();
            ItemsService = new ItemsService();
            ProgressService = new ProgressService();
            Instance = this;
        }

        [Button]
        public void SetPause(bool value)
        {
            PauseService.SetPause(value);
        }
    }
}
