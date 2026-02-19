using System;
using System.Data.SqlTypes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public float money;
    public GameObject vfx;
    private Collider2D boxCollider;
    private PlayerController playerController;

    bool inrange = false;
    private void Awake()
    {
        // Get the Box Collider from the object. 
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            inrange = true;
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
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity);
                playerController.money = Math.Clamp(playerController.money + money, 0, playerController.maxMoney);
                Destroy(gameObject);
                Destroy(clonedVFX, 1);
            }
        }
    }
}