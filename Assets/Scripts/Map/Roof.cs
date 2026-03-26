using UnityEngine;
using UnityEngine.Tilemaps;

public class Roof : MonoBehaviour
{
    public GameObject roofTileMap;
    private GameObject[] roofObjects;

    private void Awake()
    {
        roofObjects = GameObject.FindGameObjectsWithTag("Roof");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roofTileMap.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0.3f);
            foreach (GameObject mapObject in roofObjects)
            {
                mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
            roofTileMap.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
            foreach (GameObject mapObject in roofObjects)
            {
                mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
    }
}