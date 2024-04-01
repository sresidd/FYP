using UnityEngine;

public class Attack_State : Base_State<EnemyStateMachine.EnemyState>
{
    private Enemy enemy;
    private float chaseDistance;
    private LayerMask playerMask;

    private Animator animator;
    public Attack_State(Enemy enemy, float chaseDistance, LayerMask playerMask, Animator animator) : base(EnemyStateMachine.EnemyState.Attack)
    {
        this.enemy = enemy;
        this.chaseDistance = chaseDistance;
        this.playerMask = playerMask;
        this.animator = animator;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Attack State");
    }

    public override void ExitState()
    {
        // throw new System.NotImplementedException();
    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if(IsPlayerInRangeForChase()) return EnemyStateMachine.EnemyState.Chase;
        return EnemyStateMachine.EnemyState.Patrol;
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