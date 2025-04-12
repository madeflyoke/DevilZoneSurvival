using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Loot.Interfaces;
using Core.Rewards.Interfaces;
using Core.Stats.Enum;
using Core.Stats.ViewModel;
using Core.Utils;

namespace Core.Actions
{
    public class StatAppendAction : IRewardedAction
    {
        public RewardType RewardType => RewardType.STAT_CHANGED;

        public readonly StatType StatType;
        //stat operation (plus or minus)
        private readonly int _appendPercent;
        
        public StatAppendAction(StatType statType, int appendPercent)
        {
            _appendPercent = appendPercent;
            StatType = statType;
        }

        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<StatsViewModel>(out var result))
            {
                result.OnIncreasePercent(StatType, _appendPercent);
            }
        }
        
        public string FormatDescription(string sourceDescription)
        {
           return string.Format(sourceDescription, StatsHelper.GetStatName(StatType), _appendPercent);
        }
        
        public void Dispose()
        {
            
        }
    }
}
