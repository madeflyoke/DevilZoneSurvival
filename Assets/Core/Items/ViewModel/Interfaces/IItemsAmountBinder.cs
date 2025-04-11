using Core.Items.Enum;
using R3;

namespace Core.Items.ViewModel.Interfaces
{
    public interface IItemsAmountBinder
    {
        public ReadOnlyReactiveProperty<int> GetRelatedBind(ItemType itemType);
    }
}
