using UnityEngine;

public class CameraTrap : MonoBehaviour
{
    private Collider2D revealCollider;
    private AudioSource audioSource;
    private void Awake()
    {
        revealCollider = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = true;
            playerController.CamLightOn();
            audioSource.Play();
        }  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = false;
            playerController.CamLightOff();
        }
    }
}
