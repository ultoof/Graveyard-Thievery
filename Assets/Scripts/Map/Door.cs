using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public bool inrange = false;
    public BoxCollider2D trigger;
    public BoxCollider2D restriction;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            {PlayerController playerController = collision.GetComponent<PlayerController>();
            if (Keyboard.current.eKey.isPressed && playerController.key > 0)
            {
                isOpen = true;
                playerController.key--;
                restriction.enabled = false;
                trigger.enabled = false;
                spriteRenderer.enabled = false;
            }
        } 
    }
}