using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemFolder;
    public TextMeshProUGUI stealText;

    private Pickup[] spawnableItems;
    private List<GameObject> consideredItems = new List<GameObject>();

    // Unique ID for THIS spawner
    private string spawnerID;

    void Awake()
    {
        spawnableItems = itemFolder.GetComponentsInChildren<Pickup>();

        // Unique per scene + position (very important)
        spawnerID = SceneManager.GetActiveScene().name + "_" + transform.position.ToString();
    }

    void Start()
    {
        // If already collected == do NOTHING
        if (ItemTracker.collectedSpawners.Contains(spawnerID))
        {
            Debug.Log("Spawner already used: " + spawnerID);
            return;
        }

        consideredItems.Clear();

        float totalWeight = 0f;

        foreach (Pickup pickup in spawnableItems)
        {
            totalWeight += pickup.spawnWeight;
        }

        if (totalWeight <= 0)
        {
            Debug.LogWarning("No items to spawn!");
            return;
        }

        float rng = Random.Range(0, totalWeight);
        float cumulative = 0f;

        foreach (Pickup pickup in spawnableItems)
        {
            cumulative += pickup.spawnWeight;

            if (rng <= cumulative)
            {
                GameObject finalItem = Instantiate(pickup.gameObject, transform);
                Pickup finalPickup = finalItem.GetComponent<Pickup>();

                finalItem.transform.position = transform.position;
                finalPickup.stealText = stealText;

                // 🔥 Pass spawner ID to the item
                finalPickup.spawnerID = spawnerID;

                break;
            }
        }
    }
}