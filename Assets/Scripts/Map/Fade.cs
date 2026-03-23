using Unity.VisualScripting;
using UnityEngine;

public class Fade : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
    }
}