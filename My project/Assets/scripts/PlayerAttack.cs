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

        // Trigger the attack animation
        animator.SetTrigger("AttackTrigger");

        // Detect enemies in range and apply damage
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            // Ensure we're not hitting ourselves
            if (enemy.gameObject != gameObject)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("Enemy hit by player!");
                }
            }
        }

        // Reset attack after animation
        Invoke("ResetAttack", 1.0f);
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
