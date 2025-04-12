using System;
using System.Collections.Generic;
using System.Linq;
using Core.Actions.Enum;
using UnityEngine;

namespace Core.Buffs.Data
{
    [CreateAssetMenu(fileName = "BuffsConfig", menuName = "Game/BuffsConfig")]
    public class BuffsConfig : ScriptableObject
    {
        [SerializeField] private List<BuffData> _buffsData = new List<BuffData>();

        public BuffData GetBuffData(ActionType actionType)
        {
            return _buffsData.FirstOrDefault(x=>x.ActionType == actionType);
        }
        
        [Serializable]
        public class BuffData
        {
            public Sprite Icon;
            public ActionType ActionType;
            public string Title;
            public string Description;
        }
    }
}
