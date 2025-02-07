using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject helpScreen;
    public GameObject pauseMenu;

    void Update()
    {
        //  ��������/�������� ��������
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadHelp();
        }

        // ��������/�������� Pause Menu
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TogglePauseMenu();
        }

        // ��������� ��� Main Menu
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
            Time.timeScale = isActive ? 1 : 0; // ����� ���� ������� � �������
        }
    }

    void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0; // ����� ���� ������� �� �����
        }
    }




    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); // �������� �� ��������
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // ��������� ��� �����
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("Help"); // �������� �� Help
    }

}
