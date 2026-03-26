using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void RestartScene() // Resets scene.
    {
        Debug.Log("K");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeScene(string sceneName) // Changes the scene to assigned one
    {
        Debug.Log("L");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() // Shuts off the game
    {
        Application.Quit();
    }

    public void StartGame() // starts the tutorial scene everytime.
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