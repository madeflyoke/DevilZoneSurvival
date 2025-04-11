using Core.Items.Data;
using Core.Items.ViewModel;
using Core.Scripts.Utils;
using UnityEngine;

namespace Core.Services
{
    public class ItemsService
    {
        public ItemsViewConfig ViewConfig { get; private set; }
        public ItemsViewModelMediator ItemsViewModelMediator { get; private set; }

        public ItemsService()
        {
            ItemsViewModelMediator = new ItemsViewModelMediator();
            ViewConfig = Resources.Load<ItemsViewConfig>(Constants.ResourcesPaths.ItemsViewConfig);
        }
    }
}
