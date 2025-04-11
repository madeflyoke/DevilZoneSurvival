using UnityEngine;

namespace Core.Progress.ViewModel
{
    public class LevelViewModelMediator
    {
        public LevelViewModel LevelViewModel { get; }

        private readonly LevelModel _model;

        public LevelViewModelMediator()
        {
            _model = new LevelModel(1,0); //Start with 1?
            LevelViewModel = new LevelViewModel(_model);
        }
    }
}
