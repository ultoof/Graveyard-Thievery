using UnityEngine;

public class LayerControllerStatic : MonoBehaviour {
    // Properties 
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Gets component 
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100); // Fixes the sorting layer.
    }
}