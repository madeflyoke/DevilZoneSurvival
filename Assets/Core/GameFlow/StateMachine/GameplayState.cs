using Core.GameFlow.StateMachine.Interfaces;
using Core.Scripts.Utils;
using UnityEngine.SceneManagement;

namespace Core.GameFlow.StateMachine
{
    public class GameplayState : IState
    {
        private readonly IStateMachine _stateMachine;
        
        public GameplayState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            SceneManager.LoadSceneAsync(Constants.ScenesNames.Gameplay);
            //wait for death (or popup with You die title)
           // _stateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}
