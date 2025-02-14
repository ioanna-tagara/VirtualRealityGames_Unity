using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject[] assignedEnemies; // Assign enemies in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (assignedEnemies == null || assignedEnemies.Length == 0)
            {
                Debug.LogWarning("No enemies assigned to this trigger!", this);
                return;
            }

            foreach (GameObject enemy in assignedEnemies)
            {
                if (enemy == null)
                {
                    Debug.LogWarning("An assigned enemy is missing in the scene!", this);
                    continue;
                }

                EnemyAttack enemyAI = enemy.GetComponent<EnemyAttack>();
                if (enemyAI != null)
                {
                    enemyAI.Player = other.transform; // Assign the player
                    enemyAI.enabled = true; // Enable the enemy AI
                    Debug.Log($"Enemy {enemy.name} is now chasing the player!", enemy);
                }
                else
                {
                    Debug.LogWarning($"Enemy {enemy.name} is missing the EnemyAttack script!", enemy);
                }
            }
        }
    }
}
