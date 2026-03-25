using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("K");
    }

    public void ChangeScene(string sceneName)
    {
        Debug.Log("J");
        SceneManager.LoadScene(sceneName);
        Debug.Log("Halloj");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if (DataManager.instance.tutorial == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}