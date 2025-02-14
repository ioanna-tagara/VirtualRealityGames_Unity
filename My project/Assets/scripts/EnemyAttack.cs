using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public Transform Player;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public float attackRadius = 1.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer;
    private bool isAttacking;
    private int currentHealth;
    private int maxHealth = 100;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (Player == null)
            Debug.LogError("Player reference is missing!");
        if (attackPoint == null)
            Debug.LogError("AttackPoint is not assigned!");
    }

    void Update()
    {
        if (Player == null) return;

        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);
        Debug.Log("Distance to Player: " + distanceToPlayer);

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
            StopChasing();
        }

        attackTimer -= Time.deltaTime;
    }

    void ChasePlayer()
    {
        if (!agent.isOnNavMesh) return;

        agent.isStopped = false;
        agent.SetDestination(Player.position);

        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    void StopChasing()
    {
        if (!agent.isOnNavMesh) return;

        agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }

    void AttackPlayer()
    {
        if (!agent.isOnNavMesh) return;

        // Stop moving when attacking
        agent.isStopped = true;
        agent.ResetPath();

        RotateToFacePlayer();

        if (attackTimer <= 0 && !isAttacking)
        {
            Debug.Log("Enemy is attacking!");

            // Stop walking animation
            animator.SetBool("isWalking", false);

            // Trigger attack animation
            animator.SetTrigger("isAttacking");

            isAttacking = true;
            attackTimer = attackCooldown;

            DealDamage();
        }

        attackTimer -= Time.deltaTime;

        if (Vector3.Distance(Player.position, transform.position) > attackRange)
        {
            isAttacking = false;
        }
    }



    void RotateToFacePlayer()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;
        directionToPlayer.y = 0;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    void DealDamage()
    {
        if (attackPoint == null) return;

        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRadius);
        foreach (Collider hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("Player hit!");
                }
            }
        }

        isAttacking = false; // Reset attack flag
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (animator != null)
        {
            animator.SetTrigger("HitTrigger");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead!");
        Destroy(gameObject);
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
