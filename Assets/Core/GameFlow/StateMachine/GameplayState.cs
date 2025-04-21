using System;
using System.Threading;
using Core.GameFlow.StateMachine.Interfaces;
using Core.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.GameFlow.StateMachine
{
    public class GameplayState : IState, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private CancellationTokenSource _cts;
        
        public GameplayState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            SceneManager.LoadScene(Constants.ScenesNames.Gameplay);
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
