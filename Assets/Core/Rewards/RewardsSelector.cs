using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Actions;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Rewards.Data;
using Core.Rewards.Interfaces;
using Core.Stats.Enum;
using Core.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Rewards
{
    public class RewardsSelector : MonoBehaviour
    {
        [SerializeField] private RewardsConfig _rewardsConfig;
        
        private List<Type> _registeredRewardActions = new List<Type>();

        public void Start()
        {
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in allTypes)
            {
                if (typeof(IRewardedAction).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    _registeredRewardActions.Add(type);
                }
            }
        }

        public List<IRewardedAction> GetRandomRewards(int min, int max)
        {
            return GetRewards(Random.Range(min, max));
        }
        
        public List<IRewardedAction> GetRewards(int count)
        {
            var result = new List<IRewardedAction>();
            
            var rewardsResult = _registeredRewardActions.ToList();
            rewardsResult.Shuffle();

            var countDiff = count - _registeredRewardActions.Count;
            if (countDiff >0)
            {
                for (int i = 0; i < countDiff; i++)
                {
                    var randomRewardType = _registeredRewardActions[Random.Range(0, _registeredRewardActions.Count)];
                    rewardsResult.Add(randomRewardType);
                }
            }

            var statsRewardsCount = 0;
            
            for (int i = 0; i < count; i++)
            {
                switch (rewardsResult[i])
                {
                    case var stat when stat==typeof(StatAppendAction):
                        statsRewardsCount++;
                        break;
                }
            }
            
            result.AddRange(HandleStatsActionsReward(statsRewardsCount));
            
            return result;
        }
        
        //TODO Why same rewards
        private List<IRewardedAction> HandleStatsActionsReward(int count)
        {
            var stats = Enum.GetValues(typeof(StatType)).Cast<StatType>().Skip(1).ToList();

            var countDiff = count - stats.Count;
            if (countDiff >0)
            {
                for (int i = 0; i < countDiff; i++)
                {
                    var randomStatType = stats[Random.Range(0, stats.Count)];
                    stats.Add(randomStatType);
                }
            }
            
            stats.Shuffle();
            
            var targetStats = stats.GetRange(0, count);
            var result = new List<IRewardedAction>();
            
            foreach (var stat in targetStats)
            {
                var configData = _rewardsConfig.GetStatRewardData(stat);
                result.Add(new StatAppendAction(stat,
                    Random.Range(configData.MinValuePercent, configData.MaxValuePercent)));
            }

            return result;
        }
    }
}
