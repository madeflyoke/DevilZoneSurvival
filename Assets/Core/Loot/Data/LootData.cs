using System.Collections.Generic;
using Core.Actions.Interfaces;
using Core.Loot.Enums;

namespace Core.Loot.Data
{
    public class LootData
    {
        public LootType ViewType;
        public List<IAction> LootActions;
    }
}
