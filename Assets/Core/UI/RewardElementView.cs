using System;
using Core.Actions.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class RewardElementView : MonoBehaviour
    {
        public event Action<RewardElementView, IAction> Selected;
        
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _title;
        private IAction _buffAction;
        
        public void Initialize(IAction buffAction, Sprite icon, string title, string description)
        {
            _icon.sprite = icon;
            _description.text = description;
            _buffAction = buffAction;
            _title.text = title;
        }

        public void Show()
        {
            _selectButton.onClick.AddListener(OnSelect);
        }
        public void HideDestroy()
        {
            Destroy(gameObject);
        }
        
        private void OnSelect()
        {
            Selected?.Invoke(this, _buffAction);
        }
    }
}
