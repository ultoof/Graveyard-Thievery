using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void RestartScene() // Resets scene.
    {
        Debug.Log("K");
        ItemTracker.collectedSpawners.Clear();
        DataManager.instance.ResetToDefault();
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
            ItemTracker.collectedSpawners.Clear();
            DataManager.instance.ResetToDefault();
            SceneManager.LoadScene("MainScene");
        }
    }
}