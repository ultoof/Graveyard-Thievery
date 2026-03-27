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

    void Start()
    {
        upgradePrompt.text = "Press E to open the upgrade menu"; // Shows the text to enter the uppgrade menu 
    }
    void Update()
    {
        if(playerInRange && Keyboard.current.eKey.wasPressedThisFrame) // If the player is in range and is pressing E
        {
            SetUpgradeMenu(!upgradeEnabled); // If the uppgrade is not activated it turns on the menu
        }
    }


    void OnTriggerStay2D(Collider2D collision) // If player is in range 
    {
        playerInRange = true;
    }

    void SetUpgradeMenu(bool state) // Sets the state of the uppgrade menu 
    {
        upgradeEnabled = state;
        ButtonHolder.SetActive(state);
        upgradePrompt.text = state ? "" : "Press E to open the upgrade menu"; // Shows text 
    }

}
