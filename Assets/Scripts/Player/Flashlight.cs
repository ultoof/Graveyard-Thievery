using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Flashlight : MonoBehaviour
{
    // Properties
    bool isOn = false;
    public GameObject light;
    public GameObject circleLight;
    public GameObject player;
    public GameObject vfx;
    public GameObject guard;
    public GameObject iconVFX;
    public GameObject flashlightPos;
    public GameObject icon;
    public bool hasBeenUsed = false;
    private ParticleSystem iconParticle;
    private GuardEnemy guardScript;
    private float defaultDist;
    private PlayerController playerController;

    private Animator animator;

    // Setup
    void Awake()
    {
        animator = player.GetComponent<Animator>();
        guardScript = guard.GetComponent<GuardEnemy>();
        iconParticle = iconVFX.GetComponent<ParticleSystem>();
        defaultDist = guardScript.viewDistance;
        playerController = player.GetComponent<PlayerController>();

        if (DataManager.instance.canFlash)
        {
            icon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Transfer of data 
        //DataManager.instance.TransportValue = canFlash;

        // Flashlight rotation

        Vector2 lastDir = playerController.lastDir;
        Vector2 targetPosition = transform.position;
        float targetOrientation = 0f;
        if (lastDir.y > 0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y > 0.05)
        {
            targetOrientation = 0f;
            targetPosition = transform.position + new Vector3(0.04f, 0.07f, 0f);
        }
        else if (lastDir.y < -0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y < -0.05)
        {
            targetOrientation = 180f;
            targetPosition = transform.position + new Vector3(-0.08f, -0.055f, 0f);
        }
        else if (lastDir.x > 0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x > 0.05)
        {
            targetOrientation = 270f;
            targetPosition = transform.position + new Vector3(0.2f, 0f, 0f);
        }
        else if (lastDir.x < -0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x < -0.05)
        {
            targetOrientation = 90f;
            targetPosition = transform.position + new Vector3(-0.2f, 0f, 0f);
        }

        light.transform.rotation = Quaternion.Euler(0,0,targetOrientation);
        light.transform.position = targetPosition;

        // Flashlight
        if (Keyboard.current.fKey.wasPressedThisFrame && DataManager.instance.canFlash == true)
        {
            if (isOn) // The flashlight is on 
            {
                guardScript.viewDistance = defaultDist; // The guard gets normal distance
                isOn = false; // Turns off the flashlight 
                animator.SetBool("flashlight", false);
            }
            else
            {
                hasBeenUsed = true;
                guardScript.viewDistance = defaultDist * 2; // Makes the guards view distance further when the flashlight is activated. 
                isOn = true;

                // VFX
                GameObject flashVFX = Instantiate(vfx, light.transform.position, Quaternion.identity);
                iconParticle.Play();
                Object.Destroy(flashVFX, 1);
                animator.SetTrigger("light");
                animator.SetBool("flashlight", true);
            }
            light.SetActive(isOn); // Turns on the flashlight
            light.GetComponent<AudioSource>().Play();
        }
    }

    public void AddIcon()
    {
        icon.SetActive(true);
    }
}