public class ChaseState : BaseState
{
    public override void EnterState(StateMachine state)
    {
    }

    public override void PerformState(StateMachine state)
    {
        enemy.Agent.SetDestination(enemy.Player.position);
    }

    public override void ExitState(StateMachine state)
    {
    }
}
