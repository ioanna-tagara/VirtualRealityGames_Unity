using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public Dropdown difficultyDropdown;
    private int enemyDamage = 10; //Defaulty difficulty (Medium);

    void Start()
    {
        // Ensure dropdown is connected
        if (difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }

       
        SetDifficulty(difficultyDropdown.value);
    }

    void OnDifficultyChanged(int difficultyIndex)
    {
        SetDifficulty(difficultyIndex);
    }

    //sets difficulty by enemy's damage
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
        Debug.Log($"Difficulty set to {difficultyDropdown.options[difficultyIndex].text}. Enemy damage: {enemyDamage}");
    }

    public int GetEnemyDamage()
    {
        return enemyDamage;
    }
}
