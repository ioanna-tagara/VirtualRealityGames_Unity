using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("HealthPickup: Player entered pickup zone.");

            // Try to get the player's health component
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            // Check if PlayerHealth component exists
            if (ph == null)
            {
                Debug.LogError("HealthPickup: PlayerHealth script is missing on Player!");
                return; // Exit function to avoid null reference error
            }

            Debug.Log("HealthPickup: Current Health: " + ph.currentHealth);

            // Check if the player needs healing
            if (ph.currentHealth < ph.maxHealth) // Allow healing if health is not full
            {
                ph.Heal(healAmount);
                Debug.Log($"HealthPickup: Healed player for {healAmount} HP. New Health: {ph.currentHealth}");

                // Destroy the pickup object
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("HealthPickup: Player already has full health.");
            }
        }
    }
}
