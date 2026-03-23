﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator animator;
    private GameObject player;
    private bool cooldown = false;

    public GameObject deathVFX;
    public GameObject hitVFX;
    public Health health;
    public int stunned = 2;
    public LayerMask obstacleLayerMasks;
    public float viewDistance = 5f;
    public float attackRange = 2f;
    public int damage = 10;


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
                if (Vector2.Distance(transform.position, player.transform.position) < 0.5 && cooldown == false)
                {
                    StartCoroutine(Attack(1));

                    
                }
            }
        }
    }

    // Dont touch this is fix for coroutine on bullet :
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
        stunned += 2;
    }
    
    IEnumerator Attack(float duration)
    {
        health.TakeDamage(1);
        cooldown = true;
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
        yield return new WaitForSeconds(duration);
        cooldown = false;
    }
}