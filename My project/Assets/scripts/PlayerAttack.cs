using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    public float attackRange = 2f; // Range of the player's attack
    public int attackDamage = 10; // Damage dealt by the player
    public Transform attackPoint; // Point from which the attack is detected
    public LayerMask enemyLayer; // Layer for enemies

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the player's animator
    }

    void Update()
    {
        // Listen for the F key press to trigger the attack
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking) // Check if F is pressed and if not already attacking
        {
            Attack();
        }
    }

    void Attack()
    {
        // Prevent spamming the attack if it's already in progress
        isAttacking = true;

        // Trigger the attack animation (assuming you have a trigger parameter named "AttackTrigger")
        animator.SetTrigger("AttackTrigger");

        // Detect enemies in range and apply damage
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            EnemyAttack enemyHealth = enemy.GetComponent<EnemyAttack>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // Apply damage to the enemy
                Debug.Log("Enemy hit by player!");
            }
        }

        // Reset the attack state after the animation is done
        Invoke("ResetAttack", 1.0f); // Reset after 1 second (assuming the animation duration is 1 second)
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}