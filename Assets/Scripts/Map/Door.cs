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



    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            {
                Debug.Log("Staying in trigger ");
                PlayerController playerController = collision.GetComponent<PlayerController>();
                if(Keyboard.current.eKey.isPressed && playerController.key > 0)
                {
                    isOpen = true;
                    playerController.key --;
                    
                    Debug.Log("Fucking WORK");
                    restriction.enabled = false;
                    trigger.enabled = false;
                }
                else if(Keyboard.current.eKey.isPressed && playerController.key < 0)
                {
                    Debug.Log("Ts is some bs");
                }
            } 
    }
}



/*
    void Update()
    {
        if(inrange == true)
        {
            if(Keyboard.current.eKey.isPressed && playerController1.key > 0)
            {
                isOpen = true;
            }
        }
    } 

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerController1 = collision.GetComponent<PlayerController>();
        inrange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inrange = false;
    }
*/