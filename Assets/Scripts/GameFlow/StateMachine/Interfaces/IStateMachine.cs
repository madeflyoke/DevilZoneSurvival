namespace GameFlow.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        public void EnterState<T>() where T: IState;
    }
}
