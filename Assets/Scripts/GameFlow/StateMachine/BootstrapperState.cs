using GameFlow.StateMachine.Interfaces;
using UnityEngine.Device;

namespace GameFlow.StateMachine
{
    public class BootstrapperState : IState
    {
        private readonly IStateMachine _stateMachine;
        
        public BootstrapperState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            //services init (audio pools preparation etc.)
            Application.targetFrameRate = 60;
            
            _stateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
      
        }
    }
}
