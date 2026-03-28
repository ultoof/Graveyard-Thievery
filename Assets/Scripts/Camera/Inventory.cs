using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Properties
    public Image coinUI;
    public TextMeshProUGUI coinText;
    public PlayerController playerController;

    void Update()
    {
        coinUI.fillAmount = DataManager.instance.money / DataManager.instance.maxMoney;
        coinText.text = $"{math.round(coinUI.fillAmount*100)}%"; // It fills up the jar when the player picks up items. 
    }
}