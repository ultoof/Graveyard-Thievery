using UnityEngine;

public class CameraTrap : MonoBehaviour
{
    private Collider2D revealCollider;
    private void Awake()
    {
        revealCollider = GetComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = true;
        }  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = false; 
        }
    }
}
