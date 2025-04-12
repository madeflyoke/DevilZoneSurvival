using System.Collections.Generic;
using Core.Actions;
using Core.Actions.Enum;
using Core.Actions.Interfaces;
using Core.Items.ViewModel;
using Core.Services;
using Cysharp.Threading.Tasks;
using EasyButtons;
using UnityEngine;

namespace Core.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ItemView _currencyView;
        [SerializeField] private LevelExpView _levelExpView;
        [SerializeField] private LevelUpPopup _levelUpPopup;

        private void Start()
        {
            _levelExpView.Bind(ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel);
            _currencyView.Bind(ServiceLocator.Instance.ItemsService.ItemsViewModelMediator.ItemsViewModel);
        }

        [Button]
        private void InitPopup()
        {
            _levelUpPopup.Initialize(new Dictionary<ActionType, IAction>()
            {
                {ActionType.STAT_CHANGED, new MagnetRadiusChangeAction(11, 5)},
            });
            _levelUpPopup.Show();
        }
        
        private void OnDisable()
        {
           _levelExpView.Unbind();
        }
    }
}
