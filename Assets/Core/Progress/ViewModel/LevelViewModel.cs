using Core.Actions.Interfaces;
using Core.Progress.Data;
using Core.Services;
using R3;
using UnityEngine;

namespace Core.Progress.ViewModel
{
    public class LevelViewModel : IActionReceiver
    {
        private readonly LevelModel _levelModel;
        private PlayerProgressConfig ProgressConfig => _playerProgressConfig ??= ServiceLocator.Instance.ProgressService.ProgressConfig;
        private PlayerProgressConfig _playerProgressConfig;
        
        private int NextLevelExpCondition =>
            ProgressConfig.GetExpCondition(GetLevelBind().CurrentValue +
                                            1);

        private bool MaxLevel => NextLevelExpCondition == -1;
        
        public LevelViewModel(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public ReadOnlyReactiveProperty<int> GetLevelBind()
        {
            return _levelModel.CurrentLevel;
        }
        
        public ReadOnlyReactiveProperty<int> GetExpBind()
        {
            return _levelModel.CurrentExp;
        }

        public void OnLevelIncreased(int value)
        {
            _levelModel.AddLevel(value);
        }

        public void OnExpIncreased(int value)
        {
            if (MaxLevel)
            {
                Debug.LogWarning("MaxLeveled");
                return;
            }

            var remainingExp = GetExpBind().CurrentValue + value;
            var initialLevel = GetLevelBind().CurrentValue;

            while (remainingExp > 0)
            {
                var targetLevel = initialLevel + 1;
                var requiredExp = ProgressConfig.GetExpCondition(targetLevel);

                if (requiredExp!=-1 && remainingExp >= requiredExp)
                {
                    remainingExp -= requiredExp;
                    initialLevel++;
                }
                else
                {
                    break;
                }
            }

            var levelDiff = initialLevel -GetLevelBind().CurrentValue;
            if (levelDiff!=0)
            {
                OnLevelIncreased(levelDiff);
            }
            _levelModel.SetExp(remainingExp);
        }

        public int GetExpConditionToNextLevel()
        {
            return NextLevelExpCondition;
        }
    }
}
