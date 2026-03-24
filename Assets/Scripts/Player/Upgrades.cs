using System;
using JetBrains.Annotations;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public int cap = 0;
    public Text text;


    public void BuyUpgradeFlashlight()
    {
        if (DataManager.instance.canFlash == false && DataManager.instance.money >= 50)
        {
            DataManager.instance.money -= 50;
            DataManager.instance.canFlash = true;
        }
    }
    public void BuyUpgradeTaser()
    {
        if (DataManager.instance.canStun == false && DataManager.instance.money >= 50)
        {
            DataManager.instance.money -= 50;
            DataManager.instance.canStun = true;
        }
    }
    public void BuyUpgradeMaxMoneyIncrease()
    {
        if (cap <= 4 && DataManager.instance.money >= 100)
        {
            DataManager.instance.money -= 100;
            DataManager.instance.maxMoney += 50;
            cap++;
        }
    }
    public void BuyUpgradeDifficulty()
    {
        int cost = 100 + (DataManager.instance.difficulty * 50);

        if (DataManager.instance.money >= cost)
        {
            DataManager.instance.money -= cost;
            DataManager.instance.difficulty++;
            Debug.Log("Lowk works");

        }
        int newCost = 100 + (DataManager.instance.difficulty * 50);
        Debug.Log(newCost);
        // text.text = "Cost: " + newCost;
        
    }
}