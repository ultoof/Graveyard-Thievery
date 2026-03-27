using Unity.VisualScripting;
using UnityEngine;

public class Traps : MonoBehaviour
{
    // Properties
    [Header("Properties")]
    public string collidingTag;
    public bool lockPlayer = false;
    private float saveValue;


    public GameObject HiddedSprite;
    

    private PlayerController playerController;

    void Awake()
    {
        //Get Components
       playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Gets these components
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       saveValue = playerController.speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision) // If player enter radius 
    {

        if (collision.gameObject.CompareTag(collidingTag))
        {
            playerController.speed = playerController.speed/2; // Makes the player slower 
            playerController.movementRestriction = true;
        }
        else if (collision.gameObject.CompareTag(collidingTag) && lockPlayer == true)
        {
            playerController.speed = 0; 
            playerController.movementRestriction = true;
            HiddedSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // When the players leaves the radius 
    {
        if (collision.gameObject.CompareTag(collidingTag))
        {
            playerController.speed = saveValue; // Makes him normal speed
            playerController.movementRestriction = false;
        }
    }
}
