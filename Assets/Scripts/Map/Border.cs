using UnityEngine;

public class Border : MonoBehaviour
{
    public int difficulty = 0;
    public BoxCollider2D border;
    public PlayerController playerController;

    private void Awake()
    {
        border = gameObject.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        if (playerController.difficulty >= difficulty)
        {
            border.enabled = false;
        }
    }

}
