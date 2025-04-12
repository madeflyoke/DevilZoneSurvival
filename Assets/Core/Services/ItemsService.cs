using Core.Items.Data;
using Core.Items.ViewModel;
using Core.Loot.Data;
using Core.Scripts.Utils;
using UnityEngine;

namespace Core.Services
{
    public class ItemsService
    {
        public ItemsViewConfig ItemsViewConfig { get; private set; }
        public ItemsViewModelMediator ItemsViewModelMediator { get; private set; }

        public ItemsService()
        {
            ItemsViewModelMediator = new ItemsViewModelMediator();
            ItemsViewConfig = Resources.Load<ItemsViewConfig>(Constants.ResourcesPaths.ItemsViewConfig);
        }
    }
}
