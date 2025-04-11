using Core.Items.ViewModel;
using Core.Services;
using UnityEngine;

namespace Core.UI
{
    public class HudController : MonoBehaviour
    {
        //[SerializeField] private ItemView _soulsItemView;

        private void Start()
        {
        //    _soulsItemView.Bind(ServiceLocator.Instance.CurrencyService.ItemsViewModelMediator.ItemAmountBinder);
        }

        private void OnDisable()
        {
          //  _soulsItemView.Unbind();
        }
    }
}
