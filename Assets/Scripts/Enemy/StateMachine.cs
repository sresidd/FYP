using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();  

    public AttackState attackState = new AttackState();  

    void Start(){
        activeState = patrolState;
        ChangeState(activeState);
    }
    void Update(){
        if(activeState!=null){
            activeState.PerformState(this);
        }
    }
    public void ChangeState(BaseState newState){
        if(activeState!=null){
            activeState.ExitState(this);
        }

        activeState = newState;

        if(activeState!=null){
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.EnterState(this);
        }
    }
}
