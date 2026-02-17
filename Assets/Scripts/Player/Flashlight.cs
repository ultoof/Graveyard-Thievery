using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    public GameObject light;
    public GameObject circleLight;
    public GameObject player;
    public GameObject vfx;
    private Animator animator;

    // Setup
    void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Flashlight rotation
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - light.transform.position;
        diff.Normalize();

        light.transform.rotation = Quaternion.Lerp(light.transform.rotation,Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90),3f * Time.deltaTime);

        // Flashlight
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (isOn)
            {
                isOn = false;
            }
            else
            {
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