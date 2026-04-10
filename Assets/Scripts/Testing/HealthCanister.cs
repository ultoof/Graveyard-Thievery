using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]

public class HealthCanister : MonoBehaviour
{
    public int addedHealth = 1;
    bool inrange = false;
    public GameObject vfx;

    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame && inrange == true && DataManager.instance.health < 3)
        {
            DataManager.instance.health +=  addedHealth;
            GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    } 

    void OnTriggerEnter2D(Collider2D collision)
    {
        inrange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inrange = false;
    }
}
