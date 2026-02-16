using System;
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
    //public AudioSource footSteps;
    private Vector2 moveDir; // used for WASD movement
    private Vector2 movePos; // used for mouse click movement
    [HideInInspector] public Vector2 lastDir;
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
        if(moveDir.sqrMagnitude > 0.01f)
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
        // When shift key is pressed = increased movment speed : higher detection
        // When ctrl key is pressed = reduced movement speed : lower detection 
        if(Keyboard.current.shiftKey.isPressed)
        {
            rb.MovePosition(rb.position + moveDir * speed * sprintMultiplier * Time.fixedDeltaTime);
            animator.SetBool("sprint" , true);
            Debug.Log("Shift Key is being pressed");
        }
        else if (Keyboard.current.ctrlKey.isPressed)
        {
            rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
            animator.SetBool("crouch", true);
            Debug.Log("Ctrl key is pressed");
        }
        else
        {
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
            animator.SetBool("sprint" , false);
            animator.SetBool("crouch", false);
        }
        

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