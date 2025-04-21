using System;
using System.Threading;
using Core.GameFlow.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.GameFlow.StateMachine
{
    public class GameplayStateMock : IState, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private CancellationTokenSource _cts;
        
        public GameplayStateMock(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _cts = new CancellationTokenSource();
        }
        
        public async void Enter()
        {
            await UniTask.WaitUntil(() => GameplaySceneContext.Instance != null, cancellationToken:_cts.Token);

            GameplaySceneContext.Instance.CameraProvider.Initialize();
            GameplaySceneContext.Instance.Field.Initialize();
            GameplaySceneContext.Instance.PlayerCreator.Create();

            //wait for death (or popup with You die title)
            // _stateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        public void Dispose()
        {
            _cts?.Cancel();
        }
    }
}
