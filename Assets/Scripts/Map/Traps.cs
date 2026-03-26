using Unity.VisualScripting;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public string collidingTag;
    public bool lockPlayer = false;
    public GameObject HiddedSprite;
    private float saveValue;

    private PlayerController playerController;

    void Awake()
    {
        //Get Components
       playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       saveValue = playerController.speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(collidingTag))
        {
            playerController.speed = playerController.speed/2;
            playerController.movementRestriction = true;
        }
        else if (collision.gameObject.CompareTag(collidingTag) && lockPlayer == true)
        {
            playerController.speed = 0; 
            playerController.movementRestriction = true;
            HiddedSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(collidingTag))
        {
            playerController.speed = saveValue;
            playerController.movementRestriction = false;
        }
    }
}
