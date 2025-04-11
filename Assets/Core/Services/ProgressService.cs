using Core.Progress.Data;
using Core.Progress.ViewModel;
using Core.Scripts.Utils;
using UnityEngine;

namespace Core.Services
{
    public class ProgressService
    {
        public PlayerProgressConfig ProgressConfig { get; private set; }
        public LevelViewModelMediator LevelViewModelMediator { get; private set; }

        public ProgressService()
        {
            ProgressConfig = Resources.Load<PlayerProgressConfig>(Constants.ResourcesPaths.PlayerProgressConfig);
            LevelViewModelMediator = new LevelViewModelMediator();
        }
    }
}
