using Core.Actions.Interfaces;
using Core.Items.Enum;

namespace Core.Items.ViewModel.Interfaces
{
    public interface IItemsAmountChanger : IActionReceiver
    {
        public void OnIncreaseAmount(ItemType itemType, int amount);

        public void OnDecreaseAmount(ItemType itemType, int amount);
    }
}
