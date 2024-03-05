using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Patrol_State : Base_State<EnemyStateMachine.EnemyStates>
{

    private Enemy enemy;
    private float attackDistance;
    private float chaseDistance;
    private LayerMask playerMask;
    private int wayPointIndex = 0;

    public Patrol_State(Enemy enemy, float attackDistance, float chaseDistance, LayerMask playerMask) : base(EnemyStateMachine.EnemyStates.Patrol) 
    {
        this.enemy = enemy;
        this.attackDistance = attackDistance;
        this.chaseDistance = chaseDistance;
        this.playerMask = playerMask;
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

    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        if(IsPlayerInRangeForChase() && !IsPlayerInRangeForAttack()) return EnemyStateMachine.EnemyStates.Chase;
        else if(IsPlayerInRangeForAttack()) return EnemyStateMachine.EnemyStates.Attack;
        return EnemyStateMachine.EnemyStates.Patrol;
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
            enemy.Agent.SetDestination(enemy.path.waypoints[wayPointIndex].position);
        }
    }
}
