using Core.Actions.Interfaces;
using Core.Items.Enum;
using Core.ViewModelData.Interfaces;
using R3;

namespace Core.Items.ViewModel
{
    public class ItemsViewModel : IViewModel, IActionReceiver
    {
        private readonly ItemsModel _itemsModel;

        public ItemsViewModel(ItemsModel itemsModel)
        {
            _itemsModel = itemsModel;
        }

        public ReadOnlyReactiveProperty<int> GetRelatedBind(ItemType itemType)
        {
            return _itemsModel.Items[itemType];
        }
        
        public void OnIncreaseAmount(ItemType itemType, int amount)
        {
            _itemsModel.AddAmount(itemType, amount);
        }

        public void OnDecreaseAmount(ItemType itemType, int amount)
        {
            _itemsModel.RemoveAmount(itemType, amount);
        }
    }
}
