using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour 
{
    bool upgradeEnabled = false;
    bool playerInRange;
    public TextMeshProUGUI upgradePrompt;
    public GameObject ButtonHolder;

    void Update()
    {
        upgradePrompt.text = upgradeEnabled == false && playerInRange == true ? "Press E to open the upgrade menu" : "";
        if(playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SetUpgradeMenu(!upgradeEnabled);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) playerInRange = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) playerInRange = false;
    }

    void SetUpgradeMenu(bool state)
    {
        upgradeEnabled = state;
        ButtonHolder.SetActive(state);
        
    }
}
