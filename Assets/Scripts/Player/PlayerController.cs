using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    // Properties 
    public bool exposed = false;
    public int difficulty = 0;
    public float money = 0f;
    public float maxMoney = 100f;
    public float speed = 4f;
    public float sprintMultiplier = 1.5f;
    public float crouchMultiplier = 0.8f;
    public bool movementRestriction = false;
    public int Stamina = 100;
    public int key = 1;
    public bool soul = false;
    private int StaminaMod;

    private Vector2 moveDir; // used for WASD movement
    public Vector2 lastDir;
    public GameObject smokeVFX;
    public Transform flashlightPos;
    public GameObject gameOverPanel;
    public Light2D cameraLight;
    private Rigidbody2D rb;
    private Health health;
    private Animator animator;
    private ParticleSystem smokeEmitter;
    private AudioSource audioSource;
    public ParticleSystem detectedEmitter;

    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        smokeEmitter = smokeVFX.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        lastDir = Vector2.down; // Set to players starting direction
    }

    void Update()
    {
        if (health.isDead) // If the player is dead. 
        {
            return;
        }
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

        if (health.isDead) // If the player is dead 
        {
            return;
        }

        // Move with WASD
            // When shift key is pressed = increased movment speed(sprint set true, shift set false) : higher detection
            // When ctrl key is pressed = reduced movement speed(shift set true, sprint set false) : lower detection
            // The animator is sent a bool  true when the player is shifting and running and it is set to false when they are running or in a different state

            if (Keyboard.current.shiftKey.isPressed && movementRestriction == false && animator.GetBool("move") == true)// If the player runs  // Fixed it so that when standing still u cant sprint.
            {
                if (Stamina > 0) // if the player has more stamina 
                {
                    rb.MovePosition(rb.position + moveDir * speed * sprintMultiplier * Time.fixedDeltaTime);
                    animator.SetBool("sprint", true);
                    animator.SetBool("crouch", false);
                    animator.SetBool("slowed", false);
                    smokeEmitter.Play(); // Plays the FX
                    StaminaMod = -5; // Removes stamina 
                }
                else
                {
                    // Slows movement if attempt to sprint when no stamina
                    rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
                    animator.SetBool("crouch", false);
                    animator.SetBool("sprint", false);
                    animator.SetBool("slowed", true);
                    smokeEmitter.Stop(); // Stops the FX
                    StaminaMod = -100; // You run way slower due too stamina being under 0
                }

            }
            else if (Keyboard.current.ctrlKey.isPressed && movementRestriction == false) // If the player presses ctrl he crouches
            {
                rb.MovePosition(rb.position + moveDir * speed * crouchMultiplier * Time.fixedDeltaTime);
                animator.SetBool("crouch", true);
                animator.SetBool("sprint", false);
                animator.SetBool("slowed", false);
                smokeEmitter.Stop(); // Stops the fx
                StaminaMod = 5; // Gains stamina 
            }
            else
            {
                //This is just normal movement 
                rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
                animator.SetBool("sprint", false);
                animator.SetBool("crouch", false);
                animator.SetBool("slowed", false);
                smokeEmitter.Stop(); // Stops the FX
                StaminaMod = 1;
            }
        Stamina = math.clamp(Stamina + StaminaMod, -100, 1000);
    }

    public void updateFlashlight(string dir) // Checks what direction the flashlight is supposed to be directed 
    {
        if (lastDir.y > 0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y > 0.05)
        {
            flashlightPos.position = transform.position + new Vector3(0.04f, 0.07f, 0f);
        }
        else if (lastDir.y < -0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y < -0.05)
        {
            flashlightPos.position = transform.position + new Vector3(-0.08f, -0.055f, 0f);
        }
        else if (lastDir.x > 0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x > 0.05)
        {
            flashlightPos.position = transform.position + new Vector3(0.2f, 0f, 0f);
        }
        else if (lastDir.x < -0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x < -0.05)
        {
            flashlightPos.position = transform.position + new Vector3(-0.2f, 0f, 0f);
        }
    }

    public void FootStep() // Footstep function 
    {
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.Play(); // Plays the footstep audio 
    }
    public void CamLightOn() // The camera light turns on 
    {
        cameraLight.enabled = true;
        detectedEmitter.Play(); // Plays the detected vfx
    }
    public void CamLightOff() // The camera is off 
    {
        cameraLight.enabled = false;
    }
}