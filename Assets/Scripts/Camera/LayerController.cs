using UnityEngine;

public class LayerController : MonoBehaviour
{
    // Properties 
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // GEts component 
    }

    void FixedUpdate()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100); // Moves the players layer so it always is above or belove stuff like bushes and walls.
    }
}