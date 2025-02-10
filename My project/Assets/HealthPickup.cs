using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored

    private void OnTriggerEnter(Collider other)
    {
        
        // Check if the object that entered is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("its player");
            // Try to get the player's health component
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            Debug.Log("Current Health: " + ph.currentHealth);

            if (ph != null && ph.currentHealth <=(100-healAmount))
            {

                // Heal the player
                ph.Heal(healAmount);

                // Destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
