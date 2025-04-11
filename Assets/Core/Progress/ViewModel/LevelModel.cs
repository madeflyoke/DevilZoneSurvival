using R3;
using UnityEngine;

namespace Core.Progress.ViewModel
{
    public class LevelModel
    {
        public readonly ReadOnlyReactiveProperty<int> CurrentLevel;
        public readonly ReadOnlyReactiveProperty<int> CurrentExp;
        
        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExp;

        public LevelModel(int level, int exp)
        {
            _currentLevel = new ReactiveProperty<int>(level);
            _currentExp = new ReactiveProperty<int>(exp);
            
            CurrentLevel = _currentLevel;
            CurrentExp = _currentExp;
        }

        #region LEVEL

        public int GetLevel()
        {
            return _currentLevel.Value;
        }
        
        public void AddLevel(int levelCount)
        {
            SetLevel(GetLevel()+levelCount);
        }
        
        public void SetLevel(int level)
        {
            _currentLevel.Value = Mathf.Clamp(level,0,int.MaxValue);
            
            Debug.Log($"Level changed to: {_currentLevel.Value}");
        }

        #endregion

        #region EXP

        public int GetExp()
        {
            return _currentExp.Value;
        }

        public void AddExp(int exp)
        {
            SetExp(GetExp()+exp);
        }

        public void SetExp(int exp)
        {
            _currentExp.Value = Mathf.Clamp(exp,0,int.MaxValue);
            Debug.Log($"Exp changed to: {_currentExp.Value}");
        }

        #endregion
        
    }
}
