using UnityEngine;

public class Border : MonoBehaviour
{
    public int difficulty = 0;
    public BoxCollider2D border;
    public PlayerController playerController;

    private void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        border = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (playerController.difficulty > difficulty)
        {
            difficulty++;
        }
    }

}
