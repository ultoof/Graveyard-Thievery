using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    // Properties
    public string sceneLoad; 
    public bool inrange = false;
    public bool isOpen = false;
    public BoxCollider2D trigger;
    public BoxCollider2D restriction;
    public TextMeshProUGUI informingText;
    public String message;

    void OnTriggerStay2D(Collider2D collision) // If player is in range
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (Keyboard.current.eKey.isPressed) // If player presses e
            {
                if (playerController.soul == true)
                {
                    DataManager.instance.soul = true;
                }

                isOpen = true;
                restriction.enabled = false;
                Debug.Log(DataManager.instance.money);
                SceneManager.LoadScene(sceneLoad);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            informingText.text = message;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            informingText.text = "";
        }
    }
}