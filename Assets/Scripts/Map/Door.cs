using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]

public class Door : MonoBehaviour
{
    //Properties : 
    public bool isOpen = false;
    public bool needKey = true;
    public BoxCollider2D trigger;
    public BoxCollider2D restriction;
    public TextMeshProUGUI informingMessage;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    //Methods
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); // Assign sprite render at awake for future uses
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D collision) //When a trigger collision occurs with current gameobject
    {
        if (collision.CompareTag("Player")) // When its specifically a player colliding with the trigger collider
            {PlayerController playerController = collision.GetComponent<PlayerController>(); //We collect the playercontroller from the collision : in this case the player
            if (Keyboard.current.eKey.isPressed && playerController.key > 0 && needKey == true) // Checking for input, key amount and need for key 
            {
                //Disables : the restiction and the trigger, for fps. Also disables the sprite renderer
                isOpen = true;
                playerController.key--;
                restriction.enabled = false;
                trigger.enabled = false;
                spriteRenderer.enabled = false;
                informingMessage.text = "Door is now open";
                audioSource.Play();
                StartCoroutine(DoSomethingAfterDelay(3F));
            }
            else if(Keyboard.current.eKey.isPressed && needKey == false)
            {
                isOpen = true;
                restriction.enabled = false; 
                trigger.enabled = false;
                spriteRenderer.enabled = false;
                informingMessage.text = "Door is now open";
                audioSource.Play();
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



