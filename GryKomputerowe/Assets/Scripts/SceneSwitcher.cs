using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();

    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Start");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

