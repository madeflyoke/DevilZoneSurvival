using System;
using System.Threading;
using Core.GameFlow.StateMachine.Interfaces;
using Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine.Device;

namespace Core.GameFlow.StateMachine
{
    public class BootstrapperState : IState, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private CancellationTokenSource _cts;
        
        public BootstrapperState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            Application.targetFrameRate = 60;
            _cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => ServiceLocator.Instance != null, cancellationToken:_cts.Token);
            _stateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
            
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}
