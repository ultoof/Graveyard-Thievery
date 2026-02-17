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
    public float speed = 4f;
    public float sprintMultiplier = 1.5f;
    public float crouchMultiplier = 0.8f;
    public int Stamina = 100;
    private int StaminaMod;

    private Vector2 moveDir; // used for WASD movement
    public Vector2 lastDir;
    private Rigidbody2D rb;
    private Health health;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        /*
        // Read movement by right mouse click
        if (Input.GetMouseButtonDown(1))
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        */

        // Set animation params
        if (moveDir.sqrMagnitude > 0.01f)
        {
            lastDir = moveDir;
            animator.SetFloat("xMove", moveDir.x);
            animator.SetFloat("yMove", moveDir.y);
            //if (footSteps != null) footSteps.UnPause();
        }
        else
        {
            //if (footSteps != null) footSteps.Pause();
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
        if (Keyboard.current.shiftKey.isPressed && Stamina > 0)
        {
            rb.MovePosition(rb.position + moveDir * speed * sprintMultiplier * Time.fixedDeltaTime);
            animator.SetBool("sprint", true);
            animator.SetBool("crouch", false);
            StaminaMod = -10;
        }
        else if (Keyboard.current.ctrlKey.isPressed)
        {
            rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
            animator.SetBool("crouch", true);
            animator.SetBool("sprint", false);
            StaminaMod = 20;
        }
        else
        {
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
            animator.SetBool("sprint", false);
            animator.SetBool("crouch", false);
            StaminaMod = 5;
        }
        Stamina = math.clamp(Stamina + StaminaMod, 0, 1000);

        // Move with mouse position
        //rb.MovePosition(Vector2.MoveTowards(rb.position, movePos, speed * Time.fixedDeltaTime));

        /*
        // Set rotation based on moving vector
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        if(moveDir.magnitude > 0f)
            rb.rotation = angle;
        */

        /*
        // Set rotation based on mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        */
    }
}