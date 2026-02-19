using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour
{
    public NavMeshAgent nav;
    private Animator animator;
    private GameObject player;
    private Health health;
    private Rigidbody2D rb;

    public bool attacking = false;
    public int stunned = 0;
    public LayerMask obstacleLayerMasks;
    public float viewDistance;
    public GameObject vfx;
    public GameObject guardPointFolder;
    public Array guardPoints;

    private void Awake()
    {
        // Get component references
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        health = player.GetComponent<Health>();
        rb = GetComponentInParent<Rigidbody2D>();
        guardPoints = guardPointFolder.GetComponentsInChildren<Transform>();
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
        attacking = true;
        nav.isStopped = true;

        yield return new WaitForSeconds(delayTime);

        RaycastHit2D ray = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        if (stunned <= 0 && !ray)
        {
            GameObject shootVFX = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(shootVFX, 2);
            health.TakeDamage(1);
        }

        attacking = false;
        nav.isStopped = false;
    }

    //Coroutine fix on taser : 
    public void Freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration));
    }

    IEnumerator FreezeRoutine(float duration)
    {
        stunned += 2;
        nav.isStopped = true;
        yield return new WaitForSeconds(duration);
        nav.isStopped = false;
        stunned -= 2;
    }
}