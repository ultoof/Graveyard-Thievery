using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    public bool canFlash = false;
    public GameObject light;
    public GameObject circleLight;
    public GameObject player;
    public GameObject vfx;
    public GameObject guard;
    public GameObject iconVFX;
    private ParticleSystem iconParticle;
    private GuardEnemy guardScript;
    private float defaultDist;

    private Animator animator;

    void Start()
    {
        if(DataManager.instance != null)
        canFlash = DataManager.instance.canFlash;
    }

    // Setup
    void Awake()
    {
        animator = player.GetComponent<Animator>();
        guardScript = guard.GetComponent<GuardEnemy>();
        iconParticle = iconVFX.GetComponent<ParticleSystem>();
        defaultDist = guardScript.viewDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Transfer of data 
        //DataManager.instance.TransportValue = canFlash;

        // Flashlight rotation
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - light.transform.position;
        diff.Normalize();

        light.transform.rotation = Quaternion.Lerp(light.transform.rotation,Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90),3f * Time.deltaTime);

        // Flashlight
        if (Keyboard.current.fKey.wasPressedThisFrame && canFlash == true)
        {
            if (isOn)
            {
                guardScript.viewDistance = defaultDist;
                isOn = false;
            }
            else
            {
                guardScript.viewDistance = defaultDist * 2;
                isOn = true;

                // VFX
                GameObject flashVFX = Instantiate(vfx, light.transform.position, Quaternion.identity);
                iconParticle.Play();
                Object.Destroy(flashVFX, 1);
                animator.SetTrigger("light");
            }
            light.SetActive(isOn);
        }
    }
}