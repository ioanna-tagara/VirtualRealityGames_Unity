using UnityEngine;
using UnityEngine.UI;

public class GamePreferences : MonoBehaviour
{
    public Dropdown difficultyDropdown;
    public Toggle fullscreenToggle;

    void Start()
    {
        int savedDifficulty = PlayerPrefs.GetInt("Difficulty", 1); 
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1; 

        difficultyDropdown.value = savedDifficulty;
        fullscreenToggle.isOn = isFullscreen;

        ChangeDifficulty(savedDifficulty);
        SetFullscreen(isFullscreen);

        difficultyDropdown.onValueChanged.AddListener(ChangeDifficulty);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void ChangeDifficulty(int difficultyIndex)
    {
        PlayerPrefs.SetInt("Difficulty", difficultyIndex);
        PlayerPrefs.Save();
        Debug.Log("Difficulty set to: " + difficultyIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Fullscreen set to: " + isFullscreen);
    }
}
