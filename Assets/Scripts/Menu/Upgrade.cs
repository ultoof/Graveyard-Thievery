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

    void Start()
    {
        upgradePrompt.text = "Press E to open the upgrade menu";
    }
    void Update()
    {
        if(playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SetUpgradeMenu(!upgradeEnabled);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        playerInRange = true;
    }

    void SetUpgradeMenu(bool state)
    {
        upgradeEnabled = state;
        ButtonHolder.SetActive(state);
        upgradePrompt.text = state ? "" : "Press E to open the upgrade menu";
    }

}
