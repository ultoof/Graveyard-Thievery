﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator animator;
    private GameObject player;

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
        // animator.SetFloat("move", nav.velocity.magnitude);

        // Create a Linecast between this enemy and player
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
        if (!hit)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
            {
                Debug.DrawLine(gameObject.transform.position, player.transform.position);
                nav.destination = player.transform.position;
            }
        }
    }
}