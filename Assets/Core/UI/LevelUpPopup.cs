using System.Collections.Generic;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Buffs.Data;
using UnityEngine;

namespace Core.UI
{
    public class LevelUpPopup : MonoBehaviour
    {
        [SerializeField] private BuffsConfig _buffsConfig;
        [SerializeField] private BuffView _buffViewPrefab;
        [SerializeField] private RectTransform _buffsViewsParent;
        
        public void Initialize(Dictionary<ActionType, IAction> buffAction)
        {
            foreach (var kvp in buffAction)
            {
                var instance = Instantiate(_buffViewPrefab, _buffsViewsParent);
                var data = _buffsConfig.GetBuffData(kvp.Key);
                IBuffAction buff = kvp.Value as IBuffAction;
                
                instance.Initialize(kvp.Value, data.Icon, data.Title, buff.FormatDescription(data.Description));
                instance.Selected += OnBuffSelected;
                instance.Show();
            }
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnBuffSelected(BuffView buffView, IAction action)
        {
            buffView.Selected -= OnBuffSelected;
            action.TryExecute(FindObjectOfType<PlayerDummyEntity>());
            buffView.HideDestroy();
            Hide();
        }
    }
}
