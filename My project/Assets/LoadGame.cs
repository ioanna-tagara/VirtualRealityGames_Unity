using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public void PlayGame()
    { 
        SceneManager.LoadScene(1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
       mainMenu.SetActive(false);
       settingsMenu.SetActive(true);
    }

    public void BacktoMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowMenu()
    {
        Cursor.lockState = CursorLockMode.None; // Unlocks cursor
        Cursor.visible = true; // Makes cursor visible
    }


}
