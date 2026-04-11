using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    // Properties
    public TextMeshProUGUI totalMoney;
    public TextMeshProUGUI hintText;
    public String[] hints;
    public Image flashMyLight;
    public Flashlight flashlight;
    public Taser taser;

    void Awake()
    {
        DataManager.instance.totalMoney += DataManager.instance.money;
        DataManager.instance.money = 0;
    }
    void Start()
    {
        totalMoney.text = $@"$: {DataManager.instance.totalMoney}";
    } 

    public void BuyUpgradeFlashlight(Button button) // Upgrades the flashlight
    {

        if (DataManager.instance.canFlash == false && DataManager.instance.totalMoney >= 100)
        {
            DataManager.instance.difficulty++;
            DataManager.instance.totalMoney -= 100; // Spends the money 
            DataManager.instance.canFlash = true; // Allows the player to use the flashlight
            totalMoney.text = $@"$: {DataManager.instance.totalMoney}"; 
            button.interactable = false;
            flashlight.AddIcon();
        }
    }
    public void BuyUpgradeTaser(Button button) // Buys the Taser uppgrades
    {
        if (DataManager.instance.canStun == false && DataManager.instance.totalMoney >= 250) // Checks if the player can afford 
        {
            DataManager.instance.difficulty++;
            DataManager.instance.totalMoney -= 250; // Spends the money 
            DataManager.instance.canStun = true; // Unlocks the taser
            totalMoney.text = $@"$: {DataManager.instance.totalMoney}";
            button.interactable = false;
            taser.AddIcon();
        }
    }
    public void BuyUpgradeMaxMoneyIncrease(Button button) // Upgrades the money capacity 
    {
        if (DataManager.instance.upgradeCap[0] <= 4 && DataManager.instance.totalMoney >= 100) // Looks if the requriments meet 
        {
            DataManager.instance.totalMoney -= 100; // Spends the money
            DataManager.instance.maxMoney += 50; // Upgrades the storage 
            DataManager.instance.upgradeCap[0] += 1; // Adds one to the number that leads to cap
            totalMoney.text = $@"$: {DataManager.instance.totalMoney}";
        }
        if(DataManager.instance.upgradeCap[0] == 4)
        {
            button.interactable = false;
        }
    }   
    public void BuyUpgradeDifficulty(Button button) // Upgrades difficulty 
    {
        if(DataManager.instance.difficulty == 3f)
        {
            button.interactable = false; 
            return;
        }
        int cost = 100 + (DataManager.instance.difficulty * 50); // Makes the price up 
    
        if (DataManager.instance.totalMoney >= cost) // IF the player can afford 
        {
            DataManager.instance.totalMoney -= cost; // WAstes the money 
            DataManager.instance.difficulty++; // Makes the game harder 
            Debug.Log("Lowk works");
            totalMoney.text = $@"$: {DataManager.instance.totalMoney}";

        }
        int newCost = 100 + (DataManager.instance.difficulty * 50);
        Debug.Log(newCost);
        // text.text = "Cost: " + newCost; // Would make the uppgrade text change with every uppgrade
    }

    public void ChangeHintText(int hint)
    {
        hintText.text = hints[hint];
    }
}