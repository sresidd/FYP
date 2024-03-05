using UnityEngine;

public class Attack_State : Base_State<EnemyStateMachine.EnemyStates>
{
    private Enemy enemy;
    private float chaseDistance;
    private LayerMask playerMask;
    public Attack_State(Enemy enemy, float chaseDistance, LayerMask playerMask) : base(EnemyStateMachine.EnemyStates.Attack)
    {
        this.enemy = enemy;
        this.chaseDistance = chaseDistance;
        this.playerMask = playerMask;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Attack State");
    }

    public override void ExitState()
    {
        // throw new System.NotImplementedException();
    }

    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        if(IsPlayerInRangeForChase()) return EnemyStateMachine.EnemyStates.Chase;
        return EnemyStateMachine.EnemyStates.Patrol;
    }

    private bool IsPlayerInRangeForChase()
    {
        return Physics.CheckSphere(enemy.transform.position, chaseDistance, playerMask);
    }

    public override void InitializeState()
    {
        // throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        // throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider collider)
    {
        // throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(Collider collider)
    {
        // throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        // throw new System.NotImplementedException();
    }
}