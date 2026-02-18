using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour {
    private NavMeshAgent nav;
    private Animator animator;
    private GameObject player;

    public LayerMask obstacleLayerMasks;
    public float viewDistance;
    public GameObject Player;

    private void Awake()
    {
        // Get component references
        nav = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        // Uncomment if you don't need to manualy overwrite rotation
        nav.updateRotation = false;
        nav.updateUpAxis = false;

        // Get player
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        // Send data to Animator
        animator.SetFloat("xMove", nav.velocity.x);
        animator.SetFloat("yMove", nav.velocity.y);

        // Create a Linecast between this enemy and player
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
        if (!hit)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
            {
                Debug.DrawLine(gameObject.transform.position, player.transform.position);
                nav.destination = player.transform.position;
                animator.SetBool("move", true);
            }
            else
            {
                animator.SetBool("move", false);
            }
        }
        else
        {
            animator.SetBool("move", false);
        }
    }
}