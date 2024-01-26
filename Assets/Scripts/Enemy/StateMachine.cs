using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState = new();
    public ChaseState chaseState = new();  

    public AttackState attackState = new();  

    void Start(){
        activeState = patrolState;
        ChangeState(activeState);
    }
    void Update(){
        activeState?.PerformState(this);
    }
    public void ChangeState(BaseState newState){
        activeState?.ExitState(this);

        activeState = newState;

        if(activeState!=null){
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.EnterState(this);
        }
    }
}
