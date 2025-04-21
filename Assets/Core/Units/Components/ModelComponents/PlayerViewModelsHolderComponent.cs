using Core.Services;
using Core.Utils;

namespace Core.Units.Components.ModelComponents
{
    [ComponentName("PlayerViewModelsHolderComponent")]
    public class PlayerViewModelsHolderComponent : ViewModelsHolderComponent
    {
        protected override void Construct()
        {
            ViewModels.Add(ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel);
            ViewModels.Add(ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel);
            ViewModels.Add(ServiceLocator.Instance.PlayerStatsService.StatsViewModelMediator.StatsViewModel);
        }
    }
}