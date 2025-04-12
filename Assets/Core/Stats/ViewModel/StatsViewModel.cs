using Core.Actions.Interfaces;
using Core.Items.Enum;
using Core.Stats.Enum;
using R3;

namespace Core.Stats.ViewModel
{
    public class StatsViewModel : IActionReceiver
    {
        private readonly StatsModel _statsModel;

        public StatsViewModel(StatsModel statsModel)
        {
            _statsModel = statsModel;
        }

        public ReadOnlyReactiveProperty<float> GetRelatedBind(StatType stat)
        {
            return _statsModel.Stats[stat];
        }
        
        public void OnIncreasePercent(StatType stat, int valuePercent)
        {
            var current = _statsModel.GetValue(stat);
            var value = current * valuePercent / 100;
            _statsModel.AddValue(stat, value);
        }

        public void OnDecreasePercent(StatType stat, int valuePercent)
        {
            var current = _statsModel.GetValue(stat);
            var value = current * valuePercent / 100;
            _statsModel.RemoveValue(stat, value);
        }
    }
}
