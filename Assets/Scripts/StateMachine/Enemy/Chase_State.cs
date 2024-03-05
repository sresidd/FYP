using UnityEngine;
using UnityEngine.UIElements;

public class Chase_State : Base_State<EnemyStateMachine.EnemyStates>
{
    private Enemy enemy;
    private float attackDistance;
    private LayerMask playerMask;
    public Chase_State(Enemy enemy, float attackDistance, LayerMask playerMask) : base(EnemyStateMachine.EnemyStates.Chase)
    {
        this.enemy = enemy;
        this.attackDistance = attackDistance;
        this.playerMask = playerMask;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState()
    {
        // throw new System.NotImplementedException();
    }

    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        if(IsPlayerInRangeForAttack()) return EnemyStateMachine.EnemyStates.Attack;
        return EnemyStateMachine.EnemyStates.Patrol;
    }

    private bool IsPlayerInRangeForAttack()
    {
        return Physics.CheckSphere(enemy.transform.position, attackDistance, playerMask);
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