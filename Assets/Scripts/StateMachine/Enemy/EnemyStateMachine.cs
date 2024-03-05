using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyStates>
{
    public Enemy enemy;
    public float attackDistance;
    public float chaseDistance;
    public LayerMask playerMask;
    public enum EnemyStates
    {
        Patrol,
        Chase,
        Attack,
    }


    void Awake()
    {
        States.Add(EnemyStates.Patrol, new Patrol_State(enemy, attackDistance, chaseDistance, playerMask));
        States.Add(EnemyStates.Chase, new Chase_State(enemy, attackDistance, playerMask));
        States.Add(EnemyStates.Attack, new Attack_State(enemy, chaseDistance, playerMask));

        currentState = States[EnemyStates.Patrol];
    }
}
