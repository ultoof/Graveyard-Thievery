using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    // Properties 
    public float money;
    bool inrange = false;
    public string displayName;
    public GameObject vfx;
    public TextMeshProUGUI stealText;
    public float spawnWeight;
    public bool isKey;
    private PlayerController playerController;


    private void OnTriggerEnter2D(Collider2D collision) // If player is able to pick up items 
    {
        if (collision.gameObject.CompareTag("Player")) // If the player is inside the radius to pick stuff up. 
        {
            playerController = collision.gameObject.GetComponent<PlayerController>(); // Takes the collider from player
            stealText.text = $"Press E To Steal {displayName}"; // Shows the text for stealing
            inrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) { // When the player exists the radius
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = false; 
            stealText.text = "";
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame) // Checks if E is getting pressed 
        {
            if (inrange == true && DataManager.instance.money < DataManager.instance.maxMoney || inrange == true && DataManager.instance.money <= 0)
            {
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity); // Spawns the vfx
                DataManager.instance.money = math.round(Math.Clamp(DataManager.instance.money + money, 0, DataManager.instance.maxMoney)); // Adds money
                Debug.Log($"{DataManager.instance.money}");
                //DataManager.instance.money = startingMoney + playerController.money;
                Destroy(gameObject); // Destroys the item. 
                Destroy(clonedVFX, 4);

                if (isKey)
                {
                    playerController.key++; // Gives the player a key. that is needed to open some doors.
                }
            }
        }
    }
}