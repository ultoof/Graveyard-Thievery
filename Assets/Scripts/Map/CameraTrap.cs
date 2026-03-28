using UnityEngine;
using System.Collections;

public class CameraTrap : MonoBehaviour
{
    // Properties
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Gets these components 
    }

    void OnTriggerEnter2D(Collider2D collision) // If player inside radius
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            StartCoroutine(FreezeRoutine(10f,playerController));
            playerController.CamLightOn();
            audioSource.Play();
        }  
    }

    void OnTriggerExit2D(Collider2D collision) // When the player leaves the area
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.CamLightOff();
        }
    }

    IEnumerator FreezeRoutine(float duration, PlayerController playerController)
    {
        playerController.exposed = true;
        yield return new WaitForSeconds(duration);
        playerController.exposed = false;
    }
}
