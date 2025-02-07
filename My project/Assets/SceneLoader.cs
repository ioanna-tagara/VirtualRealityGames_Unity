using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject helpScreen;
    public GameObject pauseMenu;

    void Update()
    {
        //  Εμφάνιση/Απόκρυψη Βοήθειας
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadHelp();
        }

        // Εμφάνιση/Απόκρυψη Pause Menu
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TogglePauseMenu();
        }

        // Επιστροφή στο Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    void ToggleHelpScreen()
    {
        if (helpScreen != null)
        {
            bool isActive = helpScreen.activeSelf;
            helpScreen.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0; // Παύση όταν ανοίγει η βοήθεια
        }
    }

    void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0; // Παύση όταν ανοίγει το μενού
        }
    }




    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); // Φορτώνει το παιχνίδι
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Επιστροφή στο μενού
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("Help"); // Φορτώνει το Help
    }

}
