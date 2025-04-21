using UnityEngine;

namespace Core.Utils
{
    public class MonoSingleton<T>: MonoBehaviour
    {
        public static T Instance;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            
                return;
            }
        
            Instance = GetComponent<T>();
        }
    }
}
