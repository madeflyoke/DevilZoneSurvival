using Core.Items.Enum;
using Core.Items.ViewModel;
using Core.Services;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Items.Debugs
{
    public class DebugItemView : MonoBehaviour
    {
        [SerializeField] private int _changedValue;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private Image _icon;
        
        private ItemsViewModel _itemsViewModel;
        private CompositeDisposable _disposable;

        public void Start()
        {
            _icon.sprite = ServiceLocator.Instance.ItemsService.ViewConfig.GetItemConfigData(_itemType).Icon;
            Bind(ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel);
        }
        
        public void Bind(ItemsViewModel itemsViewModel)
        {
            _itemsViewModel = itemsViewModel;
            
            _addButton.onClick.AddListener(Add);
            _removeButton.onClick.AddListener(Remove);
            
            _disposable ??= new CompositeDisposable();
            itemsViewModel.GetRelatedBind(_itemType).Subscribe(RefreshText).AddTo(_disposable);
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
