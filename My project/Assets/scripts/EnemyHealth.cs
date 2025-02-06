using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public healthBar healthBar; //  Same as Player

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); //  Match Player's logic
        }
        else
        {
            Debug.LogError("Enemy HealthBar is not assigned in Inspector!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health

        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth); //  Same as Player
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
}
