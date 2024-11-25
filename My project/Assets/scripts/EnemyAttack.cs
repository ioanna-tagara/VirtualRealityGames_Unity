using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public Transform Player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;

    public GameObject weapon; // Reference to the weapon GameObject
    public Transform attackPoint; // Point from which the weapon will deal damage
    public float attackRadius = 1.0f; // Radius for attack hit detection

    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Ensure weapon reference is set
        if (weapon == null)
        {
            Debug.LogError("Weapon is not assigned! Attach a weapon GameObject to the Enemy.");
        }
    }

    void Update()
    {
        if (Player == null)
        {
            Debug.LogError("Player reference is missing! Ensure it is assigned.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Attack if within attack range
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Walk toward the player if within detection range
            ChasePlayer();
        }
        else
        {
            // Idle when out of detection range
            Idle();
        }
    }

    void ChasePlayer()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(Player.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
    }

    void AttackPlayer()
    {
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on a valid NavMesh!");
            return;
        }

        // Stop movement when in attack range
        agent.ResetPath();
        animator.SetBool("isWalking", false);

        if (attackTimer <= 0)
        {
            // Only trigger attack if still within attack range
            float distanceToPlayer = Vector3.Distance(Player.position, transform.position);
            if (distanceToPlayer <= attackRange)
            {
                animator.SetTrigger("isAttacking");
                attackTimer = attackCooldown;

                // Deal damage to the player
                DealDamage();
            }
        }

        // Decrease cooldown timer
        attackTimer -= Time.deltaTime;
    }

    void Idle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void DealDamage()
    {
        // Check if player is still in range and deal damage
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

    void OnDrawGizmosSelected()
    {
        // Visualize attack range in Scene view
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
