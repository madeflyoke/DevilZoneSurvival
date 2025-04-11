using System.Collections.Generic;
using Core.Items.Enum;
using Core.Items.ViewModel.Interfaces;

namespace Core.Items.ViewModel
{
    public class ItemsViewModelMediator
    {
        public IItemsAmountChanger ItemsAmountChanger => _viewModel;
        public IItemsAmountBinder ItemsAmountBinder => _viewModel;
        
        private readonly ItemsModel _model;
        private readonly ItemsViewModel _viewModel;
        
        public ItemsViewModelMediator()
        {
            var itemsDictionary = new Dictionary<ItemType, int>();
            foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
            {
                itemsDictionary.Add(itemType, 0);
            }
            
            _model = new ItemsModel(itemsDictionary);
            _viewModel = new ItemsViewModel(_model);
        }
    }
}
