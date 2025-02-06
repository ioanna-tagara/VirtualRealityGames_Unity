using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public healthBar healthBar; // Reference to the health bar

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // Ensure health does not go below 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Update the health bar
        healthBar.UpdateHealthBar(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        // Handle player's death (e.g., reload level, show game over screen)
        Debug.Log("Player is dead!");
        SceneManager.LoadScene("MainMenu");
    }
}