using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public event System.Action<float> OnAttack;
    private NavMeshAgent agent;
    public NavMeshAgent Agent {get => agent;}
    public LayerMask playerMask;

    public Transform Player;

    [SerializeField] 
    private string currentState;
    public Path path;

    public float enemyHealth = 100f;

    public bool playerInSightChase = false;
    public bool playerInSightAttack = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDestroy(){
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

    // private void Update()
    // {
    //     playerInSightChase = Physics.CheckSphere(transform.position,chaseDistance, playerMask);
    //     playerInSightAttack = Physics.CheckSphere(transform.position,attackDistance, playerMask);
    //     if(playerInSightChase&&!playerInSightAttack)
    //         stateMachine.ChangeState(stateMachine.chaseState);
    //     else if(playerInSightAttack&&playerInSightChase)
    //         stateMachine.ChangeState(stateMachine.attackState);
    //     else if(!playerInSightAttack&&!playerInSightChase)
    //         stateMachine.ChangeState(stateMachine.patrolState);
    // }
}
