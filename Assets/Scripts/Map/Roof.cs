using UnityEngine;
using UnityEngine.Tilemaps;

public class Roof : MonoBehaviour
{
    // Properties 
    public GameObject roofTileMap;
    private GameObject[] roofObjects;

    private void Awake()
    {
        roofObjects = GameObject.FindGameObjectsWithTag("Roof"); // Checks for this tag. 
    }

    void OnTriggerEnter2D(Collider2D collision) // If player enter radius 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roofTileMap.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0.3f); // Makes the roof tile fade
            foreach (GameObject mapObject in roofObjects)
            {
                mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f); 
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) // If the player leaves radius 
    {
            roofTileMap.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f); // Makes the roof full 
            foreach (GameObject mapObject in roofObjects)
            {
                mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
    }
}