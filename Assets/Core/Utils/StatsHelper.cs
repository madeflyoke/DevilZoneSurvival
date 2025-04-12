using System.Collections.Generic;
using Core.Stats.Enum;

namespace Core.Utils
{
    public static class StatsHelper
    {
        private static readonly Dictionary<StatType, string> StatsNames = new Dictionary<StatType, string>()
        {
            { StatType.CURRENT_MAGNET, "Magnet"},
            { StatType.GENERAL_MAXHEALTH, "Maximum Health"},
            { StatType.CURRENT_HEALTH, "Current Health"},
            { StatType.CURRENT_MOVESPEED, "Movement Speed"},
        };

        public static string GetStatName(StatType stat)
        {
            return StatsNames[stat];
        }
    }
}
