using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Transfer of data 
        //DataManager.instance.TransportValue = canFlash;

        // Flashlight rotation
        light.transform.position = flashlightPos.transform.position;
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - flashlightPos.transform.position;
        diff.Normalize();

        light.transform.rotation = Quaternion.Lerp(light.transform.rotation,Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90),3f * Time.deltaTime); // Follows the mouse 

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
            playerController.updateFlashlight("idk");
        }
    }
}