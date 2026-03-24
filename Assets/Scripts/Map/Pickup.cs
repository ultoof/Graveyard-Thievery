using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
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
        if (DataManager.instance)
        {
            startingMoney = DataManager.instance.money;
            maxMoney = DataManager.instance.maxMoney;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            if (inrange == true && playerController.money < maxMoney)
            {
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity);
                playerController.money = math.round(Math.Clamp(playerController.money + money, 0, playerController.maxMoney));
                //DataManager.instance.money = startingMoney + playerController.money;
                Destroy(gameObject);
                Destroy(clonedVFX, 4);

                if (gameObject.name == "GoldKey")
                {
                    playerController.key++;
                }
            }
        }
    }
}