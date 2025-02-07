using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Reference to the Pause Menu UI elements
    public GameObject pauseMenuUI;
    public Button saveButton;
    public Button loadButton;
    public Button preferencesButton;
    public Button continueButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Set up button listeners
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);
        preferencesButton.onClick.AddListener(Preferences);
        continueButton.onClick.AddListener(Continue);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Toggle pause menu with F2
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
    }

    public void Continue()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void Save()
    {
        // Your save logic here
        Debug.Log("Save Game");
    }

    public void Load()
    {
        // Your load logic here
        Debug.Log("Load Game");
    }

    public void Preferences()
    {
        // Your preferences logic here
        Debug.Log("Open Preferences");
        SceneManager.LoadScene("SettingsMenu");
    }

    public void QuitGame()
    {
        // Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
