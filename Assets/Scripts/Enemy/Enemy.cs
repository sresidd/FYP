using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public event System.Action<float> OnAttack;
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent {get => agent;}
    public LayerMask playerMask;

    public Transform Player;

    public float chaseDistance = 5f;
    public float attackDistance = 2f;

    [SerializeField] 
    private string currentState;
    public Path path;

    public float enemyHealth = 100f;

    public bool playerInSightChase = false;
    public bool playerInSightAttack = false;


    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnDestroy(){
        if(path==null)
            return;
        Destroy(path.gameObject);
    }

    public void TakeDamageEnemy(){

        enemyHealth -= 10f;
        Debug.Log(nameof(GameObject) + "Enemy Health : " + enemyHealth);
        if(enemyHealth<=0){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        playerInSightChase = Physics.CheckSphere(transform.position,chaseDistance, playerMask);
        playerInSightAttack = Physics.CheckSphere(transform.position,attackDistance, playerMask);
        if(playerInSightChase&&!playerInSightAttack)
            stateMachine.ChangeState(stateMachine.chaseState);
        else if(playerInSightAttack&&playerInSightChase)
            stateMachine.ChangeState(stateMachine.attackState);
        else if(!playerInSightAttack&&!playerInSightChase)
            stateMachine.ChangeState(stateMachine.patrolState);
    }
}
