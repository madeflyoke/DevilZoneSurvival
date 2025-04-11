using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Items.Enum;

namespace Core.Loot.Data
{
    public class ItemLootData
    {
        public ItemType ItemLootType;
        public List<IAction> LootActions;
    }
}
