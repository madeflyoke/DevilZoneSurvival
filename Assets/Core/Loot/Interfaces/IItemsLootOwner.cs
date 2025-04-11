using Core.Items.ViewModel;
using Core.Items.ViewModel.Interfaces;
using Core.Loot.Data;

namespace Core.Loot.Interfaces
{
    public interface IItemsLootOwner
    {
        public IItemsAmountChanger RelatedItemsAmountChanger { get; }
        
        public void ApplyLoot(ItemLootData lootData);
    }
}
