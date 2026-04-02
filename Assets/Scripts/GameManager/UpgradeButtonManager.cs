using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    public Button FlashButton;
    public Button TaseButton;
    public Button BackButton;

    void Start()
    {
        if(DataManager.instance.canFlash == true)
        {
            FlashButton.interactable = false;
        }
        else
        {
            FlashButton.interactable = true;
        }

        if(DataManager.instance.canStun == true)
        {
            TaseButton.interactable = false;
        }
        else
        {
            TaseButton.interactable = true;
        }

        if(DataManager.instance.upgradeCap[0] > 4)
        {
            BackButton.interactable  = false;
        }
        else
        {
            TaseButton.interactable = true;
        }
    }
}
