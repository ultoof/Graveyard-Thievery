using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour 
{
    // Properties 
    bool upgradeEnabled = false;
    bool playerInRange;
    public TextMeshProUGUI upgradePrompt;
    public GameObject ButtonHolder;

    void Update()
    {
        upgradePrompt.text = upgradeEnabled == false && playerInRange == true ? "Press E to open the upgrade menu" : "";
        if(playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SetUpgradeMenu(!upgradeEnabled); // If the uppgrade is not activated it turns on the menu
        }
    }


    void OnTriggerStay2D(Collider2D collision) // If player is in range 
    {
        if(collision.gameObject.CompareTag("Player")) playerInRange = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) playerInRange = false;
    }

    void SetUpgradeMenu(bool state) // Sets the state of the uppgrade menu 
    {
        upgradeEnabled = state;
        ButtonHolder.SetActive(state);
        upgradePrompt.text = state ? "" : "Press E to open the upgrade menu"; // Shows text 
    }
}
