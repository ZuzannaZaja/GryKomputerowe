using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Cursor.visible = false;
    }
    public void QuitGame()
    {
        Application.Quit();

    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");

    }
}

