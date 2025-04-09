using UnityEngine;

namespace Core.GamePlay.Units
{
    public class HealthComponent : BehaviourComponent
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }

        public void Setup(float max)
        {
            MaxHealth = max;
            CurrentHealth = max;
        }

        public void Setup(float max, float current)
        {
            MaxHealth = max;
            CurrentHealth = current;
        }

        public void UpdateValue(float value)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
        }
    }
}
