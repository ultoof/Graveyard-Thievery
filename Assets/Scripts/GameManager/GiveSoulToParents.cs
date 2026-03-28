using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GiveSoulToParents : MonoBehaviour 
{
    private bool inrange = false; // This is just to check if player is in range. On triggerstay is not rlly good at doing constant input checking
    public  TextMeshProUGUI  stealTextBoi;
    public GameObject winScreen;

    void Update()
    {
        if (inrange == true) // If player is in range, we check that in the trigger stay method.
        {
            if(Keyboard.current.eKey.wasPressedThisFrame && DataManager.instance.soul == false)
            {
                stealTextBoi.text = "Parents : You useless midget we dead gang";
                ClearTextAfterPeriod(3);
                stealTextBoi.text = "Get us our souls";

            }
            else if(Keyboard.current.eKey.wasPressedThisFrame && DataManager.instance.soul == true)
            {
                winScreen.SetActive(true);
                DataManager.instance.tutorial = false;
                DataManager.instance.canFlash = false;
                DataManager.instance.canStun = false;
                DataManager.instance.soul = false;
                DataManager.instance.money = 0;
                DataManager.instance.maxMoney = 100;
            }
        }
        else if(inrange == false)
        {
            stealTextBoi.text = "";
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
        }
    }

    IEnumerator ClearTextAfterPeriod(int time)
    {
        yield return new WaitForSeconds(time);
        stealTextBoi.text = ""; 
    }
}