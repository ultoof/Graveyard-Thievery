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
    public string dataManagerMessage;
    public String message;
    private PlayerController playerController;

    private void Update() {
        if (Keyboard.current.eKey.isPressed && inrange)
        {
            if (playerController.soul == true)
            {
                DataManager.instance.soul = true;
            }

            if (dataManagerMessage != null)
            {
                DataManager.instance.position = dataManagerMessage;
            }

            isOpen = true;
            restriction.enabled = false;
            Debug.Log(DataManager.instance.money);
            DataManager.instance.health = 3;
            SceneManager.LoadScene(sceneLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag("Player"))
        {
            informingText.text = message;
            inrange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            informingText.text = "";
            inrange = false;
        }
    }
}