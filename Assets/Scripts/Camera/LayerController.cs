using UnityEngine;

public class LayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}