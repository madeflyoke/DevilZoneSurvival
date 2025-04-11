using Core.Items.Enum;
using Core.Items.ViewModel;
using Core.Progress.ViewModel;
using R3;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class LevelExpView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _expText;
        
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private TMP_Text _levelText;
        
        private LevelViewModel _levelViewModel;
        private CompositeDisposable _disposable;
        
        public void Bind(LevelViewModel levelViewModel)
        {
            _levelViewModel = levelViewModel;
            
            _disposable ??= new CompositeDisposable();
            _levelViewModel.GetExpBind().Subscribe(RefreshBar).AddTo(_disposable);
            _levelViewModel.GetLevelBind().Subscribe(RefreshLevel).AddTo(_disposable);
        }
        
        public void Unbind()
        {
            _disposable?.Dispose();
        }

        private void RefreshBar(int value)
        {
            _progressBar.ChangeValue(value);
            _expText.text = "Exp: " +value + "/" + _levelViewModel.GetExpConditionToNextLevel() ; //TODO Remove
        }

        private void RefreshLevel(int value)
        {
            _levelText.text = "Level "+value;
            _progressBar.RefreshMaxValue(_levelViewModel.GetExpConditionToNextLevel());
        }
    }
}
