using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar; // Reference to the health bar

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned in PlayerHealth script!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // Prevent further damage if already dead

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth); // Update health bar
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is dead! Reloading MainMenu...");
        SceneManager.LoadScene("MainMenu");
    }
}
