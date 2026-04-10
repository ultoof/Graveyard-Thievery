using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour
{
    // Properties
    public NavMeshAgent nav;
    private Animator animator;
    private GameObject player;
    private Health health;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private AudioSource audioSource;

    public int currentPoint = 1;
    public bool searching = true;
    public bool attacking = false;
    public bool stoppedAtPoint = false;
    public int stunned = 0;
    public LayerMask obstacleLayerMasks;
    public float viewDistance;
    public GameObject vfx;
    public GameObject guardPointFolder;
    public GameObject hitVFX;
    public GameObject deathVFX;
    public Transform[] guardPoints;

    private bool isStoppingAtPoint = false;
    private void Awake()
    {
        // Get component references
        player = GameObject.Find("Player");
        if(player == null)
        {
            Debug.LogError("Player cannot be found");
            return;
        }
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        health = player.GetComponent<Health>();
        guardPoints = guardPointFolder.GetComponentsInChildren<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Uncomment if you don't need to manualy overwrite rotation
        nav.updateRotation = false;
        nav.updateUpAxis = false;
    }

    void Update()
    {
         
         nav.speed = playerController.exposed ? 3 : 2; //If the player is exposed : 3 if not 2    

        if (!stoppedAtPoint && DataManager.instance.health > 0) // Checks if the player is still alive and the guard isn't at a guard point.
        {
            // Create a Linecast between this enemy and player
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

            // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
            if (!hit && !attacking || playerController.exposed && !attacking)
            {
                animator.SetFloat("xMove", nav.velocity.x);
                animator.SetFloat("yMove", nav.velocity.y);
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < 1.5) // Checks if player is in attacking range
                {
                    StartCoroutine(Attack(2.0f));
                    searching = false;
                }
                else if (distance < viewDistance || playerController.exposed && !attacking)
                {
                    // If the player is in view distance but also isn't getting attacked
                    Debug.DrawLine(gameObject.transform.position, player.transform.position);
                    nav.destination = player.transform.position;
                    animator.SetBool("move", true);
                    searching = false;
                }
                // 
                else
                {
                    MoveToGuardPoint(); // Moves to the assigned guard points
                } 
            }
            else
            {
                MoveToGuardPoint();
            }
        }
    }

        private void MoveToGuardPoint() // The assigned guard points is where the guard goes when it can't detect the player.
        {
            nav.destination = guardPoints[currentPoint].position;
            animator.SetFloat("xMove", nav.velocity.x);
            animator.SetFloat("yMove", nav.velocity.y);
            animator.SetBool("move", true);
            searching = true;

            float pointDistance = Vector2.Distance(transform.position, guardPoints[currentPoint].position);

            if (pointDistance <= 1 && !isStoppingAtPoint)
            {
                StartCoroutine(StopAtPoint(2.5f)); // stops at a point for a 5 sec(can be adjusted).
                currentPoint = (currentPoint < guardPoints.Length - 1) ? currentPoint + 1 : 1; // Advance guard to next spot
            }
        }
        
        
        IEnumerator Attack(float delayTime) // The attack funktion
        {
        attacking = true;
        nav.isStopped = true;
        animator.SetBool("shoot", true);

        yield return new WaitForSeconds(delayTime); // Just to delay its attack speed.

        RaycastHit2D ray = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        if (stunned <= 0 && !ray) // If the player stuns the guard. 
        {
            GameObject shootVFX = Instantiate(vfx, transform.position, Quaternion.identity); // Spawns the vfx
            Destroy(shootVFX, 4);
            health.TakeDamage(1); // Deal damage

            if (DataManager.instance.health >= 1)
            {
                GameObject hitVFXClone = Instantiate(hitVFX, player.transform.position, Quaternion.identity);
                Destroy(hitVFXClone, 2);
            }
            else
            {
                GameObject hitVFXClone = Instantiate(deathVFX, player.transform.position, Quaternion.identity);
                player.SetActive(false);
                Destroy(hitVFXClone, 2);
            }
        }

        attacking = false;
        nav.isStopped = false;
        animator.SetBool("shoot", false); // Stops shooting 
    }

    //Coroutine fix on taser : 
    public void Freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration));
    }

    public void StepSound() // Plays a sound.
    {
        audioSource.Play();
    }

    IEnumerator FreezeRoutine(float duration) // The stun funktion. Makes the guard stop for a few sec.
    {
        stunned += 2;
        nav.isStopped = true;
        yield return new WaitForSeconds(duration);
        nav.isStopped = false;
        stunned -= 2;
    }

    IEnumerator StopAtPoint(float duration) // // This makes the guard 
    {
        animator.SetBool("move", false);
        stoppedAtPoint = true;

        yield return new WaitForSeconds(duration);
        stoppedAtPoint = false;
    }
}

  