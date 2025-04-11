using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Progress.Data
{
    [CreateAssetMenu(fileName = "PlayerProgressConfig", menuName = "Game/PlayerProgressConfig")]
    public class PlayerProgressConfig : ScriptableObject
    {
        [SerializeField] private List<PlayerProgressConditionData> _playerProgressConditions;

        public int GetExpCondition(int level)
        {
            var progressData = _playerProgressConditions.FirstOrDefault(x => level == x.Level);
            if (progressData == null)
            {
                return -1;
            }
            return progressData.ExpCondition;
        }

        [Serializable]
        public class PlayerProgressConditionData
        {
            public int Level = 0;
            public int ExpCondition = 1;
        }
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (_playerProgressConditions.GroupBy(x=>x.Level).Count()!=_playerProgressConditions.Count)
            {
                Debug.LogWarning("Same level id in ProgressConfig");
            }
        }

#endif
    }
}
