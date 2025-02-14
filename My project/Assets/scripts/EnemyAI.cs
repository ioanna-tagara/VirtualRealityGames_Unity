using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Auto-assign the player if not set in Inspector
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }
    }

    public void StartChasing()
    {
        if (player != null)
        {
            isChasing = true;
            agent.isStopped = false;
            animator.SetBool("isWalking", true);
        }
    }
}
