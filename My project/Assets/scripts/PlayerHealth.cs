using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar; // Reference to the health bar
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned in PlayerHealth script!");
        }
    }

    public int getCurrentHealth(int currenthealth) { return currentHealth; }

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



    //palio die
    /* void Die()
     {
         animator.SetTrigger("DieTrigger");
         GetComponent<Collider>().enabled = false;
         GetComponent<NavMeshAgent>().enabled = false;

         Debug.Log("Player is dead! Reloading MainMenu...");
         SceneManager.LoadScene("MainMenu");
     } */

    void Die()
    {
        // Παίξε το animation θανάτου
        animator.SetTrigger("DieTrigger");

        // Απενεργοποίησε το Collider και τον NavMeshAgent για να αποτρέψεις την αλληλεπίδραση
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        // Αν υπάρχει CharacterController, απενεργοποίησέ τον
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        Debug.Log("Player is dead! Reloading MainMenu...");

        // Καθυστέρηση για τη μετάβαση στο MainMenu ώστε να ολοκληρωθεί το animation
        StartCoroutine(ReloadMainMenu());
    }

    IEnumerator ReloadMainMenu()
    {
        // Δώσε χρόνο στο animation να ολοκληρωθεί (5 δευτερόλεπτα)
        yield return new WaitForSeconds(5f);

        // Φόρτωσε τη σκηνή MainMenu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth); // Update health bar
        }
        Debug.Log("Player healed! Current Health: " + currentHealth);
    }
}
