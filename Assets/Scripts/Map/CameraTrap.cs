using UnityEngine;

public class CameraTrap : MonoBehaviour
{
    private Collider2D revealCollider;
    private void Awake()
    {
        revealCollider = GetComponent<PolygonCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = true;
        }  
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.exposed = false; 
        }
       
    }

    void Update()
    {
        
        
    }
}
