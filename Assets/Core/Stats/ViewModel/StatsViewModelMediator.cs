using System.Collections.Generic;
using Core.Stats.Enum;
using UnityEngine;

namespace Core.Stats.ViewModel
{
    public class StatsViewModelMediator
    {
        public readonly StatsViewModel StatsViewModel;
        private readonly StatsModel _statsModel;
        
        public StatsViewModelMediator()
        {
            _statsModel = new StatsModel(new Dictionary<StatType, float>()
            {
                { StatType.GENERAL_MAXHEALTH, 100 },
                { StatType.CURRENT_HEALTH, 100 },
                { StatType.CURRENT_MOVESPEED, 2 },
                { StatType.CURRENT_MAGNET, 3 },
            });
            StatsViewModel = new StatsViewModel(_statsModel);
        }
    }
}
