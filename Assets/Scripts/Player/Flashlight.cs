using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    bool canFlash = true;
    public GameObject light;
    public GameObject circleLight;
    public GameObject player;
    public GameObject vfx;
    public GameObject guard;
    private GuardEnemy guardScript; 

    private Animator animator;

    // Setup
    void Awake()
    {
        animator = player.GetComponent<Animator>();
        guardScript = guard.GetComponent<GuardEnemy>();
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
                guardScript.viewDistance = 3f;
                isOn = false;
            }
            else
            {
                guardScript.viewDistance = 5f;
                isOn = true;

                // VFX
                GameObject flashVFX = Instantiate(vfx, light.transform.position, Quaternion.identity);
                Object.Destroy(flashVFX, 1);
                animator.SetTrigger("light");
            }
            light.SetActive(isOn);
        }
    }
}