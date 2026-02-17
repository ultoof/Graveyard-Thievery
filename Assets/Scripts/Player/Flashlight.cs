using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    public GameObject light;
    public GameObject player;

    private PlayerController playerController;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        if(Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (isOn)
            {
                isOn = false;
            }
            else
            {
                isOn = true;
            }
            light.SetActive(isOn);
        }

        if(playerController.lastDir.y > 0)
        {
            light.transform.rotation = Quaternion.Euler(0,0,0);
            light.transform.position = transform.position + new Vector3(0,0.27f,0);
        }
        else if(playerController.lastDir.y < 0)
        {
            light.transform.rotation = Quaternion.Euler(0,0,180);
            light.transform.position = transform.position + new Vector3(0,-0.25f,0);
        }
         else if(playerController.lastDir.x > 0)
        {
            light.transform.rotation = Quaternion.Euler(0,0,270);
            light.transform.position = transform.position + new Vector3(0.1f,0,0);
        }
        else if(playerController.lastDir.x < 0)
        {
            light.transform.rotation = Quaternion.Euler(0,0,90);
            light.transform.position = transform.position + new Vector3(-0.1f,0,0);
        }

    }

    void FixedUpdate()
    {
        
    }

}
