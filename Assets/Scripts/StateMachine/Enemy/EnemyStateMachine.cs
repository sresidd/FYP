using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public Enemy enemy;
    public float attackDistance;
    public float chaseDistance;
    public LayerMask playerMask;
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
    }


    void Awake()
    {
        States.Add(EnemyState.Patrol, new Patrol_State(enemy, attackDistance, chaseDistance, playerMask));
        States.Add(EnemyState.Chase, new Chase_State(enemy, attackDistance, playerMask));
        States.Add(EnemyState.Attack, new Attack_State(enemy, chaseDistance, playerMask));

        currentState = States[EnemyState.Patrol];
    }
}
