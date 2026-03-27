using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Properties 
    public GameObject itemFolder;
    public TextMeshProUGUI stealText;
    private Pickup[] spawnableItems;
    private List<GameObject> consideredItems = new List<GameObject>();

    void Awake()
    {
        spawnableItems = itemFolder.GetComponentsInChildren<Pickup>(); // Gets the component in the child
        Debug.Log(spawnableItems);
    }

    void Start()
    {
        float rng = UnityEngine.Random.Range(0.1f, 100f); // Gives out the random number 
        foreach (Pickup pickup in spawnableItems)
        {
            if (pickup.spawnWeight >= rng)
            {
                consideredItems.Add(pickup.gameObject); // Adds the gameobject
            }
        }

        int rngFinal = UnityEngine.Random.Range(1, consideredItems.Count);

        GameObject finalItem = Instantiate(consideredItems[rngFinal], gameObject.transform);
        Pickup finalPickup = finalItem.GetComponent<Pickup>(); // Makes the item avalible too pickup.

        finalItem.transform.position = gameObject.transform.position;
        finalPickup.stealText = stealText; // Shows the steal text.
    }
}