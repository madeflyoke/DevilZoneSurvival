using System;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Items.Enum;
using Core.Items.ViewModel;
using Core.Loot.Interfaces;

namespace Core.Actions
{
    public class ItemsCountAppendAction : IAction
    {
        private readonly ItemType _itemType;
        private readonly int _additionalCount;
        
        public ItemsCountAppendAction(ItemType itemType, int additionalCount)
        {
            _itemType = itemType;
            _additionalCount = additionalCount;
        }
        
        public void TryExecute(IActionReceiversOwner owner)
        {
            if (owner.TryGetActionReceiver<ItemsViewModel>(out var result))
            {
                result.OnIncreaseAmount(_itemType, _additionalCount);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}
