using UnityEditor;
using UnityEngine;

public class Patrol_State : Base_State<EnemyStateMachine.EnemyState>
{

    private Enemy enemy;
    private float attackDistance;
    private float chaseDistance;
    private LayerMask playerMask;
    private int wayPointIndex = 0;

    private Animator animator;

    private Vector3 direction;
    public Patrol_State(Enemy enemy, float attackDistance, float chaseDistance, LayerMask playerMask, Animator animator) : base(EnemyStateMachine.EnemyState.Patrol) 
    {
        this.enemy = enemy;
        this.attackDistance = attackDistance;
        this.chaseDistance = chaseDistance;
        this.playerMask = playerMask;
        this.animator = animator;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Patrol state");
        // Implement entering logic for patrol state
    }

    public override void ExitState()
    {
        // Implement exiting logic for patrol state
    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if(IsPlayerInRangeForChase() && !IsPlayerInRangeForAttack()) return EnemyStateMachine.EnemyState.Chase;
        else if(IsPlayerInRangeForAttack()) return EnemyStateMachine.EnemyState.Attack;
        return EnemyStateMachine.EnemyState.Patrol;
    }

    private bool IsPlayerInRangeForChase()
    {
        return Physics.CheckSphere(enemy.transform.position, chaseDistance, playerMask);
    }

    private bool IsPlayerInRangeForAttack()
    {
        return Physics.CheckSphere(enemy.transform.position, attackDistance, playerMask);
    }


    public override void InitializeState()
    {
        // Debug.Log("Initializing Patrol state");
        // Implement initialization logic for patrol state
    }

    public override void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Trigger entered in Patrol state");
        // Implement trigger logic for patrol state
    }

    public override void OnTriggerExit(Collider collider)
    {
        // Debug.Log("Trigger exited in Patrol state");
        // Implement trigger exit logic for patrol state
    }

    public override void OnTriggerStay(Collider collider)
    {
        // Debug.Log("Trigger stayed in Patrol state");
        // Implement trigger stay logic for patrol state
    }

    public override void UpdateState()
    {
        // Debug.Log("Updating Patrol state");
    // Implement update logic for patrol state
        // Vector3 enemyVelocity = enemy.Agent.velocity.normalized;

        animator.SetFloat("Y",  .5f);
        // animator.SetFloat("Y", direction.z * .5f);

        PatrolCycle();
    }

    private void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            if(wayPointIndex < enemy.path.waypoints.Count - 1)
            {
                wayPointIndex ++;
            }
            else
                wayPointIndex = 0;

            // direction = (enemy.transform.position - enemy.path.waypoints[wayPointIndex].position).normalized;
            enemy.Agent.SetDestination(enemy.path.waypoints[wayPointIndex].position);
        }
    }
}
