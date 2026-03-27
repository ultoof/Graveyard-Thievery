using System;
using JetBrains.Annotations;
using TMPro;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    // Properties 
    private int cap = 0;
    public TextMeshProUGUI totalMoney;
    public Image flashMyLight;

    void Start()
    {
        totalMoney.text = $"{DataManager.instance.money}";
    } 

    public void BuyUpgradeFlashlight() // Upgrades the flashlight
    {
        if (DataManager.instance.canFlash == false && DataManager.instance.money >= 100)
        {
            DataManager.instance.money -= 100; // Spends the money 
            DataManager.instance.canFlash = true; // Allows the player to use the flashlight
            totalMoney.text = $"{DataManager.instance.money}";
        }
    }
    public void BuyUpgradeTaser() // Buys the Taser uppgrades
    {
        if (DataManager.instance.canStun == false && DataManager.instance.money >= 250) // Checks if the player can afford 
        {
            DataManager.instance.money -= 250; // Spends the money 
            DataManager.instance.canStun = true; // Unlocks the taser
            totalMoney.text = $"{DataManager.instance.money}";
        }
    }
    public void BuyUpgradeMaxMoneyIncrease() // Upgrades the money capacity 
    {
        if (cap <= 4 && DataManager.instance.money >= 100) // Looks if the requriments meet 
        {
            DataManager.instance.money -= 100; // Spends the money
            DataManager.instance.maxMoney += 50; // Upgrades the storage 
            cap++; // Adds one to the number that leads to cap
            totalMoney.text = $"{DataManager.instance.money}";
        }
    }
    public void BuyUpgradeDifficulty() // Upgrades difficulty 
    {
        int cost = 100 + (DataManager.instance.difficulty * 50); // Makes the price up 

        if (DataManager.instance.money >= cost) // IF the player can afford 
        {
            DataManager.instance.money -= cost; // WAstes the money 
            DataManager.instance.difficulty++; // Makes the game harder 
            Debug.Log("Lowk works");
            totalMoney.text = $"{DataManager.instance.money}";

        }
        int newCost = 100 + (DataManager.instance.difficulty * 50);
        Debug.Log(newCost);
        // text.text = "Cost: " + newCost; // Would make the uppgrade text change with every uppgrade
        
    }
}