using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public bool needKey = true;
    public BoxCollider2D trigger;
    public BoxCollider2D restriction;
    public TextMeshProUGUI informingMessage;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            {PlayerController playerController = collision.GetComponent<PlayerController>();
            if (Keyboard.current.eKey.isPressed && playerController.key > 0 && needKey == true)
            {
                isOpen = true;
                playerController.key--;
                restriction.enabled = false;
                trigger.enabled = false;
                spriteRenderer.enabled = false;
                informingMessage.text = "Door is now open";
                StartCoroutine(DoSomethingAfterDelay(3F));
            }
            else if(Keyboard.current.eKey.isPressed && needKey == false)
            {
                isOpen = true;
                restriction.enabled = false; 
                trigger.enabled = false;
                spriteRenderer.enabled = false;
                informingMessage.text = "Door is now open";
                StartCoroutine(DoSomethingAfterDelay(3F));
            }
            else if(Keyboard.current.eKey.isPressed && playerController.key <= 0)
            {
                informingMessage.text = "Door is locked, you need a key";
                StartCoroutine(DoSomethingAfterDelay(3f)); 
            }
        } 
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            informingMessage.text = "";
        }
    }

      IEnumerator DoSomethingAfterDelay(float timer)
    {
        yield return new WaitForSeconds(timer); 
        informingMessage.text = "";
    }
}



