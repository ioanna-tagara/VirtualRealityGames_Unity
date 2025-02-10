using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player
        if (other.CompareTag("Player"))
        {
            // Try to get the player's health component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Heal the player
                playerHealth.Heal(healAmount);

                // Destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
