public class PatrolState : BaseState
{
    public int wayPointsIndex;
    public override void EnterState(StateMachine state)
    {
    }
    public override void PerformState(StateMachine state)
    {
        PatrolCycle();
    }
    public override void ExitState(StateMachine state)
    {
    }
    public void PatrolCycle(){
        if(enemy.Agent.remainingDistance<.2f){
            if(wayPointsIndex<enemy.path.waypoints.Count - 1)
                wayPointsIndex++;
            else
                wayPointsIndex = 0;
            enemy.Agent.SetDestination(enemy.path.waypoints[wayPointsIndex].position);
        }
    }
}
