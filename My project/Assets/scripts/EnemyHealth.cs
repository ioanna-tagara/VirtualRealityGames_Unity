using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar; // Reference to health bar
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            Debug.LogError($"HealthBar is not assigned for {gameObject.name}!");
        }

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // Prevent further damage if already dead

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
         Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");
      
        //animator.SetTrigger("HitTrigger");
        
        //Debug.Log("hit animation!!!!!!");



        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("DieTrigger");
        Debug.Log($"{gameObject.name} has died!");      
        isDead = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        // Destroy the enemy
        Destroy(gameObject, 5.0f);

    }
}
