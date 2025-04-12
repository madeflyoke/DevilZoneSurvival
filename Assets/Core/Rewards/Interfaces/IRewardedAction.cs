using Core.Actions.Enum;
using Core.Actions.Interfaces;

namespace Core.Rewards.Interfaces
{
    public interface IRewardedAction : IAction
    {
        public RewardType RewardType { get; }
        public string FormatDescription(string sourceDescription);
    }
}
