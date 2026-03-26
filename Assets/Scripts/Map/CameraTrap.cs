using UnityEngine;

public class CameraTrap : MonoBehaviour
{
    // Properties
    private Collider2D revealCollider;
    private AudioSource audioSource;
    private void Awake()
    {
        revealCollider = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>(); // Gets these components 
    }

    void OnTriggerEnter2D(Collider2D collision) // If player inside radius
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = true; // Makes the player exposed 
            playerController.CamLightOn(); 
            audioSource.Play();
        }  
    }

    void OnTriggerExit2D(Collider2D collision) // When the player leaves the area
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = false; // Makes him not exposed
            playerController.CamLightOff();
        }
    }
}
