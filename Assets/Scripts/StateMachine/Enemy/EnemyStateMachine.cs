using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public Enemy enemy;
    public float attackDistance;
    public float chaseDistance;
    public LayerMask playerMask;

    public Animator animator;
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
    }


    void Awake()
    {
        States.Add(EnemyState.Patrol, new Patrol_State(enemy, attackDistance, chaseDistance, playerMask, animator));
        States.Add(EnemyState.Chase, new Chase_State(enemy, attackDistance, playerMask, animator));
        States.Add(EnemyState.Attack, new Attack_State(enemy, chaseDistance, playerMask, animator
        
        ));

        currentState = States[EnemyState.Patrol];
    }
}
