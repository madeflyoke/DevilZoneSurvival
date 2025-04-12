using System.Collections.Generic;
using Core.Stats.Enum;
using Core.Stats.ViewModel;

namespace Core.Services
{
    public class PlayerStatsService
    {
        public StatsViewModelMediator StatsViewModelMediator { get; private set; }

        public PlayerStatsService()
        {
            StatsViewModelMediator = new StatsViewModelMediator();
        }
    }
}
