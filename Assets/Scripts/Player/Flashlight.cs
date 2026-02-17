using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    public GameObject light;
    public GameObject circleLight;
    private Light2D circleLightComponent;
    public GameObject player;
    public GameObject vfx;
    private Transform currentOffset;
    private Animator animator;

    private PlayerController playerController;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        circleLightComponent = circleLight.GetComponent<Light2D>();
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Flashlight rotation
        if (playerController.lastDir.y > 0)
        {
            light.transform.rotation = Quaternion.Euler(0, 0, 0);
            light.transform.position = transform.position + new Vector3(0, 0.27f, 0);
        }
        else if (playerController.lastDir.y < 0)
        {
            light.transform.rotation = Quaternion.Euler(0, 0, 180);
            light.transform.position = transform.position + new Vector3(0, -0.25f, 0);
        }
        else if (playerController.lastDir.x > 0)
        {
            light.transform.rotation = Quaternion.Euler(0, 0, 270);
            light.transform.position = transform.position + new Vector3(0.1f, 0, 0);
        }
        else if (playerController.lastDir.x < 0)
        {
            light.transform.rotation = Quaternion.Euler(0, 0, 90);
            light.transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        }

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
