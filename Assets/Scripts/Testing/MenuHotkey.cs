using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuHotkey : MonoBehaviour {
    void Update() {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}