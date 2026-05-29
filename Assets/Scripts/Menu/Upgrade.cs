using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour 
{
    // Properties 
    bool upgradeEnabled = false;
    bool playerInRange;
    public TextMeshProUGUI upgradePrompt;
    public TextMeshProUGUI hintText;
    public GameObject ButtonHolder;
    public GameObject moneyText;
    public PlayerController playerController;

    void Update()
    {
        upgradePrompt.text = upgradeEnabled == false && playerInRange == true ? "Press E to open the upgrade menu" : "";
        if (playerInRange || upgradeEnabled)
        {
            upgradePrompt.text = upgradeEnabled ? "Press E to close" : "Press E to open the upgrade menu";
        }
        else
        {
            upgradePrompt.text = "";
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && (playerInRange || upgradeEnabled))
        {
            SetUpgradeMenu(!upgradeEnabled);
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
        moneyText.SetActive(state);
        hintText.text = state ? "Shadow: What do you need?" : "";
        playerController.speed = state ? 0f : 1.8f;
    }
}
