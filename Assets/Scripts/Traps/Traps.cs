using Unity.VisualScripting;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public string collidingTag;
    public GameObject player;
    private float saveValue;
    private Health health;
    private PlayerController playerController;

    void Awake()
    {
        //Get Components
       health = player.GetComponent<Health>();
       playerController = player.GetComponent<PlayerController>();
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
            health.TakeDamage(20);
            playerController.speed = playerController.speed/2;
            playerController.movementRestriction = true;
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
