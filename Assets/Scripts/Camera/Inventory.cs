using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image coinUI;
    public TextMeshProUGUI coinText;
    public PlayerController playerController;

    void Update()
    {
        coinUI.fillAmount = playerController.money / playerController.maxMoney;
        coinText.text = $"{math.round(coinUI.fillAmount*100)}%";
    }
}