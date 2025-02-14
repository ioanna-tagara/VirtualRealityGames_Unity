using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public Dropdown difficultyDropdown;
    private int enemyDamage;

    void Start()
    {
        // Ensure dropdown is connected
        if (difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);

            // Load saved difficulty or default to Medium (index 1)
            int savedDifficulty = PlayerPrefs.GetInt("DifficultyIndex", 1);
            difficultyDropdown.value = savedDifficulty;

            // Apply the saved difficulty
            SetDifficulty(savedDifficulty);
        }
    }

    void OnDifficultyChanged(int difficultyIndex)
    {
        SetDifficulty(difficultyIndex);
        PlayerPrefs.SetInt("DifficultyIndex", difficultyIndex); // Save selected difficulty
        PlayerPrefs.Save(); // Ensure it is stored
    }

    // Sets difficulty by enemy's damage
    void SetDifficulty(int difficultyIndex)
    {
        switch (difficultyIndex)
        {
            case 0: // Easy
                enemyDamage = 5;
                break;
            case 1: // Medium
                enemyDamage = 10;
                break;
            case 2: // Hard
                enemyDamage = 15;
                break;
        }

        PlayerPrefs.SetInt("EnemyDamage", enemyDamage);
        PlayerPrefs.Save(); // Save enemy damage
        Debug.Log($"Difficulty set to {difficultyDropdown.options[difficultyIndex].text}. Enemy damage: {enemyDamage}");
    }

    public int GetEnemyDamage()
    {
        return enemyDamage;
    }
}
