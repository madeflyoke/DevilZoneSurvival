using System.Collections.Generic;
using Core.Items.Enum;

namespace Core.Items.ViewModel
{
    public class ItemsViewModelMediator
    {
        public ItemsViewModel ItemsViewModel { get; }

        private readonly ItemsModel _model;

        public ItemsViewModelMediator()
        {
            var itemsDictionary = new Dictionary<ItemType, int>();
            foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
            {
                itemsDictionary.Add(itemType, 0);
            }
            
            _model = new ItemsModel(itemsDictionary);
            ItemsViewModel = new ItemsViewModel(_model);
        }
    }
}
