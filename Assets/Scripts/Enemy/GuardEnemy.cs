using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator animator;
    private GameObject player;
    private Health health;

    public bool attacking = false;
    public LayerMask obstacleLayerMasks;
    public float viewDistance;
    public GameObject vfx;


    private void Awake()
    {
        // Get component references
        player = GameObject.Find("Player");
        nav = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = player.GetComponent<Health>();
    }

    void Start()
    {
        // Uncomment if you don't need to manualy overwrite rotation
        nav.updateRotation = false;
        nav.updateUpAxis = false;
    }


    void Update()
    {
        // Send data to Animator
        animator.SetFloat("xMove", nav.velocity.x);
        animator.SetFloat("yMove", nav.velocity.y);

        // Create a Linecast between this enemy and player
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
        if (!hit && !attacking)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < 2)
            {
                
                attacking = true;
                StartCoroutine(Attack(2.0f));
            }
            else if (distance < viewDistance)
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

    IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GameObject shootVFX = Instantiate(vfx, transform.position, Quaternion.identity);
        GameObject.Destroy(shootVFX, 2);

        attacking = false;
        health.TakeDamage(1);
        Debug.Log("Attacked");
    }
}