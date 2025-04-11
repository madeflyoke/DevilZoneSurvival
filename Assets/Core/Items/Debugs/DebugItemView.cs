using Core.Items.Enum;
using Core.Items.ViewModel.Interfaces;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Items.Debugs
{
    public class DebugItemView : MonoBehaviour
    {
        public ItemType ItemType => _itemType;
        
        [SerializeField] private int _changedValue;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private Image _icon;
        
        private IItemsAmountChanger _itemsViewModel;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Initialize(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
        
        public void Bind(IItemsAmountChanger amountChanger, IItemsAmountBinder binder)
        {
            _itemsViewModel = amountChanger;
            
            _addButton.onClick.AddListener(Add);
            _removeButton.onClick.AddListener(Remove);
            
            _disposable ??= new CompositeDisposable();
            binder.GetRelatedBind(_itemType).Subscribe(RefreshText).AddTo(_disposable);
        }
        
        public void Unbind()
        {
            _addButton.onClick.RemoveListener(Add);
            _removeButton.onClick.RemoveListener(Remove);
            _disposable?.Dispose();
        }

        private void RefreshText(int value)
        {
            _itemCountText.text = value.ToString();
        }
        
        private void Add()
        {
            _itemsViewModel.OnIncreaseAmount(_itemType, _changedValue);
        }

        private void Remove()
        {
            _itemsViewModel.OnDecreaseAmount(_itemType, _changedValue);
        }
    }
}
