using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public int money;
    private Collider2D boxCollider;
    bool inrange = false;
    private void Awake()
    {
        // Get the Box Collider from the object. 
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = true;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.money += money;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = false;
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (inrange == true)
            {

                Destroy(gameObject);
            }
        }
    }
    
}