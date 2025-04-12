using System;
using System.Collections.Generic;
using System.Linq;
using Core.Actions;
using Core.Actions.Enum;
using Core.Rewards.Interfaces;
using Core.Stats.Enum;
using UnityEngine;

namespace Core.Rewards.Data
{
    [CreateAssetMenu(fileName = "RewardsConfig", menuName = "Game/RewardsConfig")]
    public class RewardsConfig : ScriptableObject
    {
        [SerializeField] private List<StatRewardData> _statsRewards;

        public RewardData GetRelatedRewardData(IRewardedAction rewardedAction)
        {
            switch (rewardedAction.RewardType)
            {
                case RewardType.STAT_CHANGED:
                    var statAction = rewardedAction as StatAppendAction;
                    return GetStatRewardData(statAction.StatType);
            }

            return null;
        }
        
        public StatRewardData GetStatRewardData(StatType stat)
        {
            return _statsRewards.FirstOrDefault(x=>x.StatType == stat);
        }
        
        [Serializable]
        public abstract class RewardData
        {
            public Sprite Icon;
            public string Title;
            public string Description;
        }
        
        [Serializable]
        public class StatRewardData : RewardData
        {
            public StatType StatType;
            public int MinValuePercent;
            public int MaxValuePercent;
        }
    }
}
