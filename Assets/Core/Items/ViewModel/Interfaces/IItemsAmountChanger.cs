using Core.Items.Enum;

namespace Core.Items.ViewModel.Interfaces
{
    public interface IItemsAmountChanger
    {
        public void OnIncreaseAmount(ItemType itemType, int amount);

        public void OnDecreaseAmount(ItemType itemType, int amount);
    }
}
