using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    bool isOn = false;
    public GameObject light;
    public Transform player;
    public Transform  flashlight;

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

        flashlight.rotation = player.rotation;
    }

    void FixedUpdate()
    {
        
    }

}
