using Core.Items.ViewModel.Interfaces;
using Core.Loot.Data;
using Core.Loot.Interfaces;
using Core.Services;
using UnityEngine;

namespace Core.Loot
{
    public class PlayerItemsLootOwner : MonoBehaviour, IItemsLootOwner
    {
        public IItemsAmountChanger RelatedItemsAmountChanger =>
            ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsAmountChanger;
        
        public void ApplyLoot(ItemLootData lootData)
        {
            RelatedItemsAmountChanger.OnIncreaseAmount(lootData.ItemType, lootData.Count);
        }
    }
}
