using System.Collections.Generic;
using System.Linq;
using Core.Stats.Enum;
using R3;
using UnityEngine;

namespace Core.Stats.ViewModel
{
    public class StatsModel
    {
        public readonly Dictionary<StatType, ReadOnlyReactiveProperty<float>> Stats;
        
        private readonly Dictionary<StatType, ReactiveProperty<float>> _stats;

        public StatsModel(Dictionary<StatType, float> stats)
        {
            _stats = stats.ToDictionary(pair=>pair.Key, pair=>new ReactiveProperty<float>(pair.Value));
            Stats = _stats.ToDictionary(pair => pair.Key, pair => pair.Value.ToReadOnlyReactiveProperty());
        }
        
        public void AddValue(StatType stat, float value)
        {
            var currentValue = GetValue(stat);
            if (currentValue!=-1)
            {
                SetValue(stat,currentValue+value);
            }
        }

        public void RemoveValue(StatType stat, float value)
        {
            var currentValue = GetValue(stat);
            if (currentValue!=-1)
            {
                SetValue(stat,currentValue-value);
            }
        }
        
        public float GetValue(StatType stat)
        {
            if (_stats.TryGetValue(stat, out var value))
            {
                return value.Value;
            }
            return -1;
        }
        
        private void SetValue(StatType stat, float value)
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            _stats[stat].Value = value;
            
            Debug.Log($"Stat {stat} has been changed, now: {_stats[stat].Value}");
        }
    }
}
