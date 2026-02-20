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
        if (!stoppedAtPoint)
        {
            // Create a Linecast between this enemy and player
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

            // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
            if (!hit && !attacking)
            {
                animator.SetFloat("xMove", nav.velocity.x);
                animator.SetFloat("yMove", nav.velocity.y);
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < 1.5)
                {
                    StartCoroutine(Attack(2.0f));
                    searching = false;
                }
                else if (distance < viewDistance)
                {
                    Debug.DrawLine(gameObject.transform.position, player.transform.position);
                    nav.destination = player.transform.position;
                    animator.SetBool("move", true);
                    searching = false;
                }
                else
                {
                    MoveToGuardPoint();
                }
            }
            else
            {
                MoveToGuardPoint();
            }
        }

        void MoveToGuardPoint()
        {
            nav.destination = guardPoints[currentPoint].position;
            animator.SetBool("move", true);
            searching = true;

            float pointDistance = Vector2.Distance(transform.position, guardPoints[currentPoint].position);

            if (pointDistance <= 1)
            {
                StartCoroutine(StopAtPoint(5f));
                if (currentPoint != guardPoints.Length - 1)
                {
                    currentPoint++;
                }
                else
                {
                    currentPoint = 1;
                }
            }
        }
    }

    IEnumerator Attack(float delayTime)
    {
        attacking = true;
        nav.isStopped = true;
        animator.SetBool("shoot", true);

        yield return new WaitForSeconds(delayTime);

        RaycastHit2D ray = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        if (stunned <= 0 && !ray)
        {
            GameObject shootVFX = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(shootVFX, 2);
            health.TakeDamage(1);

            if (health.health >= 1)
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
        animator.SetBool("shoot", false);
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

    IEnumerator StopAtPoint(float duration)
    {
        animator.SetBool("move", false);
        stoppedAtPoint = true;

        yield return new WaitForSeconds(duration);
        stoppedAtPoint = false;
    }
}