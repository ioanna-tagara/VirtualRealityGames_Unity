using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    public Transform Player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage;
    public GameObject weapon;
    public Transform attackPoint;
    public float attackRadius = 1.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer;
    private bool isAttacking;
    private bool isIdle;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        attackDamage = PlayerPrefs.GetInt("EnemyDamage",10); //set damage by selected difficulty or default (10 for medium diff) otherwise
       
        currentHealth = maxHealth;
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

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // Αν ο εχθρός είναι ήδη νεκρός, μην κάνεις τίποτα.

        // Μείωσε το health
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Αποφυγή αρνητικής τιμής.

        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        // Ενεργοποίηση του Hit animation
        if (animator != null)
        {
            animator.SetTrigger("HitTrigger");
            Debug.Log("Hit animation triggered.");
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }

        // Έλεγχος αν πέθανε
        if (currentHealth <= 0)
        {
            Die();
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
                        playerHealth.TakeDamage(attackDamage); // Apply damage to the player
                        Debug.Log("Player hit by weapon!");
                    }
                }
                else if (hit.CompareTag("Enemy") && hit.gameObject != gameObject) // Prevents self-hit
                {
                    EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(attackDamage); // Apply damage to another enemy
                        Debug.Log("Enemy hit!");
                    }
                }
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead!");
        Destroy(gameObject); // Destroy the enemy GameObject on death
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
