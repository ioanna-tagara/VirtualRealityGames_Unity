using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public Transform Player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;

    public GameObject weapon;
    public Transform attackPoint;
    public float attackRadius = 1.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer;
    private bool isAttacking;
    private bool isIdle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (weapon == null)
        {
            Debug.LogError("Weapon is not assigned! Attach a weapon GameObject to the Enemy.");
        }

        if (Player == null)
        {
            Debug.LogError("Player reference is missing! Ensure it is assigned.");
        }
    }

    void Update()
    {
        if (Player == null) return;

        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer()
    {
        // Reset attack state
        isAttacking = false;
        isIdle = false;
        attackTimer = 0; // Reset attack cooldown

        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.SetDestination(Player.position);

            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false); // Ensure attack animation stops
            animator.SetBool("isIdle", false); // Ensure attack animation stops
        }
    }

    void AttackPlayer()
    {
        if (!agent.isOnNavMesh) return;

        // Stop moving when attacking
        agent.isStopped = true;
        agent.ResetPath();

        // Face the player
        RotateToFacePlayer();

        // Check if already attacking
        if (isAttacking && Vector3.Distance(Player.position, transform.position) > attackRange)
        {
            // Stop attacking if player moves out of range
            StopAttack();
            return;
        }

        animator.SetBool("isWalking", false); // Stop walking animation

        // Trigger attack when cooldown is over
        if (attackTimer <= 0 && !isAttacking)
        {
            animator.SetTrigger("isAttacking");
            isAttacking = true;
            attackTimer = attackCooldown;

            // Apply damage
            DealDamage();
        }

        // Cooldown timer
        attackTimer -= Time.deltaTime;

        // Reset attacking if the player moves away
        if (Vector3.Distance(Player.position, transform.position) > attackRange)
        {
            StopAttack();
        }
    }

    void StopAttack()
    {
        isAttacking = false; // Reset attacking state
        animator.SetBool("isAttacking", false); // Ensure attack animation stops
    }

    void Idle()
    {
        // Reset attack state
        isAttacking = false;
        attackTimer = 0; // Reset cooldown

        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void RotateToFacePlayer()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep rotation horizontal
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    void DealDamage()
    {
        // Check if the player is still in range before dealing damage
        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);
        if (distanceToPlayer <= attackRange)
        {
            Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRadius);
            foreach (Collider hit in hitObjects)
            {
                if (hit.CompareTag("Player"))
                {
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log("Player hit by weapon!");
                    }
                }
            }
        }
    }

    public void OnFootstep()
    {
       
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
