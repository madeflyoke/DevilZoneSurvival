using Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Items.Debugs
{
    public class DebugItemsController : MonoBehaviour
    {
        [SerializeField] private DebugItemView _debugItemView;

        private void Start()
        {
            var mediator = ServiceLocator.Instance.ItemsService.ItemsViewModelMediator;
            _debugItemView.Initialize(ServiceLocator.Instance.ItemsService.ViewConfig.GetItemConfigData(_debugItemView.ItemType).Icon);
            _debugItemView.Bind(mediator.ItemsAmountChanger, mediator.ItemsAmountBinder);
        }
    }
}
