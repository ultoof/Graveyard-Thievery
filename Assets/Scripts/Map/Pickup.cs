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
    private PlayerController playerController;
    private float startingMoney = 0;
    private float maxMoney = 100;

    private void Awake()
    {
        if (DataManager.instance) // Checks the money it would add. 
        {
            startingMoney = DataManager.instance.money;
            maxMoney = DataManager.instance.maxMoney;
        }
    }

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
            if (inrange == true && playerController.money < maxMoney || inrange == true && money <= 0)
            {
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity); // Spawns the vfx
                playerController.money = math.round(Math.Clamp(playerController.money + money, 0, playerController.maxMoney)); // Adds money 
                //DataManager.instance.money = startingMoney + playerController.money;
                Destroy(gameObject); // Destroys the item. 
                Destroy(clonedVFX, 4);

                if (gameObject.name == "GoldKey")
                {
                    playerController.key++; // Gives the player a key. that is needed to open some doors.
                }
            }
        }
    }
}