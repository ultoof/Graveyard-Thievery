using Unity.VisualScripting;
using UnityEngine;

public class Fade : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision) // Checks if player is in range 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f); // Fades out the color contrast
        }
    }

    void OnTriggerExit2D(Collider2D collision) // When player leaves the area 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); // Makes the color full contrast again.
        }
    }
}