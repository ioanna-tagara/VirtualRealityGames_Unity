using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SceneLoader : MonoBehaviour
{
    // Reference to the Pause Menu UI elements
    public GameObject pauseMenuUI;
    public Button saveButton;
    public Button loadButton;
    public Button preferencesButton;
    public Button continueButton;
    public Button quitButton;
    public GameObject player; // Reference to the player GameObject
    private string saveFilePath;
    // Reference to the Help Screen
    public GameObject helpScreen;
    private PlayerHealth playerHealth;

    // Reference to the camera and player movement scripts
    public MonoBehaviour playerMovement;  // Player movement script (e.g., ThirdPersonController)
    public MonoBehaviour cameraController; // Camera control script (e.g., ThirdPersonCam)

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);
        helpScreen.SetActive(false);

        // Set up button listeners for the pause menu
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);
        preferencesButton.onClick.AddListener(Preferences);
        continueButton.onClick.AddListener(Continue);
        quitButton.onClick.AddListener(QuitGame);

        // Ensure player exists
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>(); // Get the PlayerHealth script
        }

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component is missing from the player GameObject.");
        }

        saveFilePath = Application.persistentDataPath + "/savegame.json";
    }

    void Update()
    {
        // Toggle Help screen with F1
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleHelpScreen();
        }

        // Toggle Pause menu with F2
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TogglePauseMenu();
        }

        // Return to Main Menu with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    // Toggle Help screen visibility
    void ToggleHelpScreen()
    {
        if (helpScreen != null)
        {
            bool isActive = helpScreen.activeSelf;
            helpScreen.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0; // Pause the game when the help screen is active
            isPaused = !isActive; // Update pause state

            // Disable or enable the camera and player movement during pause
            if (playerMovement != null)
                playerMovement.enabled = !isActive;

            if (cameraController != null)
                cameraController.enabled = !isActive;
        }
    }

    // Toggle Pause menu visibility and freeze/unfreeze the game
    void TogglePauseMenu()
    {
        if (pauseMenuUI != null)
        {
            bool isActive = pauseMenuUI.activeSelf;
            pauseMenuUI.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0; // Pause the game when the pause menu opens
            isPaused = !isActive; // Update pause state
            // Show/hide cursor when paused
            Cursor.visible = !isActive;
            Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;

            // Disable or enable the camera and player movement during pause
            if (playerMovement != null)
                playerMovement.enabled = !isActive;

            if (cameraController != null)
                cameraController.enabled = !isActive;
        }
    }

    // Resume the game
    public void Continue()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game

        // Re-enable the camera and player movement
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (cameraController != null)
            cameraController.enabled = true;
    }

    // Save the game
    public void Save()
    {
        if (player == null || playerHealth == null)
        {
            Debug.LogError("Player or PlayerHealth is missing.");
            return; // Ensure player and playerHealth are valid
        }

        // Create a GameData object to store the player's information
        GameData data = new GameData
        {
            playerPositionX = player.transform.position.x,
            playerPositionY = player.transform.position.y,
            playerPositionZ = player.transform.position.z,
            playerHealth = playerHealth.currentHealth // Use actual player health from PlayerHealth
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved: " + saveFilePath);
    }

    // Load the game
    public void Load()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        if (player != null)
        {
            player.transform.position = new Vector3(data.playerPositionX, data.playerPositionY, data.playerPositionZ);
            if (playerHealth != null)
            {
                playerHealth.currentHealth = data.playerHealth; // Restore player health
            }
        }

        Debug.Log("Game Loaded!");
    }

    // Open Preferences (e.g., load a settings menu scene)
    public void Preferences()
    {
        Debug.Log("Open Preferences");
        SceneManager.LoadScene("SettingsMenu"); // Replace with your actual preferences scene
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Load the Game scene
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene"); // Replace with your actual game scene
    }

    // Load Main Menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene
    }

    // Load Help scene
    public void LoadHelp()
    {
        SceneManager.LoadScene("Help"); // Replace with your actual help scene
    }

    [System.Serializable]
    public class GameData
    {
        public float playerPositionX;
        public float playerPositionY;
        public float playerPositionZ;
        public int playerHealth;
    }
}
