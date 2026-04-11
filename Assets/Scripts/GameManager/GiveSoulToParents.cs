using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GiveSoulToParents : MonoBehaviour 
{
    private bool inrange = false; // This is just to check if player is in range. On triggerstay is not rlly good at doing constant input checking
    public  TextMeshProUGUI  stealTextBoi;
    public GameObject winScreen;
    public WinScreen winScript;

    void Update()
    {
        if (inrange && Keyboard.current.eKey.wasPressedThisFrame && DataManager.instance.soul) // If player is in range, we check that in the trigger stay method.
        {
            winScreen.SetActive(true);
            winScript.StartCutscene();
            DataManager.instance.tutorial = false;
            DataManager.instance.canFlash = false;
            DataManager.instance.canStun = false;
            DataManager.instance.soul = false;
            DataManager.instance.money = 0;
            DataManager.instance.maxMoney = 100;
            DataManager.instance.position = "Default";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = true;
            if(DataManager.instance.soul == false)
            {
                stealTextBoi.text = "Get us our souls";
            }
            else
            {
                stealTextBoi.text = "Press E To Return The Souls";
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
            stealTextBoi.text = "";
        }
    }
}