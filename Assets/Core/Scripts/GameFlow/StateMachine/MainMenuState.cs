using Core.Scripts.GameFlow.StateMachine.Interfaces;
using Core.Scripts.Utils;
using UnityEngine.SceneManagement;

namespace Core.Scripts.GameFlow.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly IStateMachine _stateMachine;
        
        public MainMenuState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            SceneManager.LoadScene(Constants.ScenesNames.MainMenu);
            //wait for Play button (subscribing?)
            _stateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}
