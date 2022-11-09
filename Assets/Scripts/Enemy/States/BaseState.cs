public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;
    public abstract void EnterState(StateMachine state);
    public abstract void PerformState(StateMachine state);
    public abstract void ExitState(StateMachine state);
}
