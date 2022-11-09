public class AttackState : BaseState
{
    bool attacked = false;
    
    public override void EnterState(StateMachine state)
    {
        enemy.Player.GetComponent<PlayerHealth>().TakeDamage(1f);
    }
    public override void PerformState(StateMachine state)
    {
    }
    public override void ExitState(StateMachine state)
    {
    }
}
