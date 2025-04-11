using Core.Items.Enum;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Items.ViewModel
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private Image _icon; //from config?
        
        private CompositeDisposable _disposable;
        
        public void Bind(ItemsViewModel itemViewModel)
        {
            _disposable ??= new CompositeDisposable();
            itemViewModel.GetRelatedBind(_itemType).Subscribe(RefreshText).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable?.Dispose();
        }
        
        private void RefreshText(int value)
        {
            _itemCountText.text = value.ToString();
        }
    }
}
