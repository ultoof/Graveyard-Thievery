using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public float money;
    public string displayName;
    public GameObject vfx;
    public TextMeshProUGUI stealText;
    public float spawnWeight;
    public bool isKey;

    [HideInInspector] public string spawnerID;

    private bool inrange = false;
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            stealText.text = $"Press E To Steal {displayName}";
            inrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
            stealText.text = "";
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && inrange)
        {
            if (DataManager.instance.money < DataManager.instance.maxMoney || DataManager.instance.money <= 0)
            {
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity);

                DataManager.instance.money = math.round(
                    Mathf.Clamp(DataManager.instance.money + money, 0, DataManager.instance.maxMoney)
                );

                Destroy(clonedVFX, 4);

                if (isKey)
                {
                    playerController.key++;
                }

                // 🔥 Mark THIS SPAWNER as used
                ItemTracker.collectedSpawners.Add(spawnerID);

                Destroy(gameObject);
            }
        }
    }
}