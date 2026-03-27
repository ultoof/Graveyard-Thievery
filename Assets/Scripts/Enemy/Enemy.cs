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
            if (Vector2.Distance(transform.position, player.transform.position) < viewDistance) // If the player is in the range of the smaller zombie
            {
                Debug.DrawLine(gameObject.transform.position, player.transform.position); // Makes a line the zombie can see.
                nav.destination = player.transform.position;
                if (Vector2.Distance(transform.position, player.transform.position) < 0.5 && cooldown == false)
                {
                    StartCoroutine(Attack(1)); // Attacks player 

                    
                }
            }
        }
    }

    // Dont touch this is fix for coroutine on bullet :
    public void Freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration)); // Stuns the small zombie for a little bit.
    }

    IEnumerator FreezeRoutine(float duration) // How long the zombie should be stunned
    {
        stunned += 2;
        nav.isStopped = true;
        yield return new WaitForSeconds(duration); // How long it should wait 
        nav.isStopped = false;
        stunned += 2;
    }
    
    IEnumerator Attack(float duration) // A attacking funktion
    {
        health.TakeDamage(1); // How much damage it will deal. 
        cooldown = true;
         if (health.health >= 1)
            {
                GameObject hitVFXClone = Instantiate(hitVFX, player.transform.position, Quaternion.identity); // Spawns in a vfx
                Destroy(hitVFXClone, 2); // Destroys the vfx. 
            }
            else
            {
                GameObject hitVFXClone = Instantiate(deathVFX, player.transform.position, Quaternion.identity); // Spawns in a death fx
                player.SetActive(false); 
                Destroy(hitVFXClone, 2); // Destroys the fx
            }
        yield return new WaitForSeconds(duration);
        cooldown = false; // Makes him not have a cooldown. 
    }
}