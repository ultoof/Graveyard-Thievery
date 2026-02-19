using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public float money;
    bool inrange = false;
    public string displayName;
    public GameObject vfx;
    public TextMeshProUGUI stealText;
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            stealText.text = $"Press E To Steal {displayName}";
            inrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = false;
            stealText.text = "";
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