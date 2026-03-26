using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour 
{
    bool upradeEnabled = false;
    public TextMeshProUGUI upgradePrompt;
    public GameObject flashMe;
    public GameObject stunMe;
    public GameObject bagMe;
    public GameObject difficultyMe;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Keyboard.current.eKey.isPressed)
            {
                upradeEnabled = true;
            }

            if (upradeEnabled)
            {
                upgradePrompt.text = "";
                flashMe.SetActive(true);
                stunMe.SetActive(true);
                bagMe.SetActive(true);
                difficultyMe.SetActive(true);
            }
            else
            {
                upgradePrompt.text = "Press E to open the upgrade menu";
            }
        }
    }
}