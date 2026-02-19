using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public int money = 0;
    public int maxMoney = 100;
    public float speed = 4f;
    public float sprintMultiplier = 1.5f;
    public float crouchMultiplier = 0.8f;
    public bool movementRestriction = false;
    public int Stamina = 100;
    private int StaminaMod;
    

    private Vector2 moveDir; // used for WASD movement
    public Vector2 lastDir;
    public GameObject smokeVFX;
    private Rigidbody2D rb;
    private Health health;
    private Animator animator;
    private ParticleSystem smokeEmitter;
    
    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        smokeEmitter = smokeVFX.GetComponent<ParticleSystem>();
        lastDir = Vector2.down; // Set to players starting direction
    }

    void Update()
    {
        if (health.isDead)
            return;

        // Get movement by WASD
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Normalize Vector
        if (moveDir.magnitude > 1f)
            moveDir.Normalize();

        // Set animation params
        if (moveDir.sqrMagnitude > 0.01f)
        {
            lastDir = moveDir;
            animator.SetFloat("xMove", moveDir.x);
            animator.SetFloat("yMove", moveDir.y);
        }

        animator.SetBool("move", moveDir.sqrMagnitude > 0.01f ? true : false);
    }

    void FixedUpdate()
    {

        if (health.isDead)
            return;

        // Move with WASD
        // When shift key is pressed = increased movment speed(sprint set true, shift set false) : higher detection
        // When ctrl key is pressed = reduced movement speed(shift set true, sprint set false) : lower detection
        // The animator is sent a bool  true when the player is shifting and running and it is set to false when they are running or in a different state

        if (Keyboard.current.shiftKey.isPressed && movementRestriction == false)
        {
            if (Stamina > 0)
            {
                rb.MovePosition(rb.position + moveDir * speed * sprintMultiplier * Time.fixedDeltaTime);
                animator.SetBool("sprint", true);
                animator.SetBool("crouch", false);
                animator.SetBool("slowed", false);
                smokeEmitter.Play();
                StaminaMod = -5;
            }
            else
            {
                // Slows movement if attempt to sprint when no stamina
                rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
                animator.SetBool("crouch", false);
                animator.SetBool("sprint", false);
                animator.SetBool("slowed", true);
                smokeEmitter.Stop();
                StaminaMod = -100;
            }

        }
        else if (Keyboard.current.ctrlKey.isPressed && movementRestriction == false)
        {
            rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
            animator.SetBool("crouch", true);
            animator.SetBool("sprint", false);
            animator.SetBool("slowed", false);
            smokeEmitter.Stop();
            StaminaMod = 5;
        }
        else
        {
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
            animator.SetBool("sprint", false);
            animator.SetBool("crouch", false);
            animator.SetBool("slowed", false);
            smokeEmitter.Stop();
            StaminaMod = 1;
        }
        Stamina = math.clamp(Stamina + StaminaMod, -100, 1000);
    }
}