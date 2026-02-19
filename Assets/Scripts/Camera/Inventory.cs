using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image coinUI;
    public PlayerController playerController;

    void Update()
    {
        coinUI.fillAmount = playerController.money / playerController.maxMoney;
    }
}