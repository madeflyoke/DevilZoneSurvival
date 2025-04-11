using Core.Items.ViewModel;
using Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private LevelExpView _levelExpView;

        private void Start()
        {
            _levelExpView.Bind(ServiceLocator.Instance.ProgressService.LevelViewModelMediator.LevelViewModel);
        }

        private void OnDisable()
        {
           _levelExpView.Unbind();
        }
    }
}
