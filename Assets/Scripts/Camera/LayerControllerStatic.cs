using UnityEngine;

public class LayerControllerStatic : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}