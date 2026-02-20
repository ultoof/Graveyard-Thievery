using Unity.VectorGraphics;
using UnityEngine;

public class Upgrades : MonoBehaviour 
{
    public int cap = 0;

    public void BuyUpgradeFlashlight()
    {
        if(DataManager.instance.canFlash == false)
        {
            DataManager.instance.money -= 50;
            DataManager.instance.canFlash = true;  
        }
    }
    public void BuyUpgradeTaser()
    {
        if(DataManager.instance.canStun == false)
        {
            DataManager.instance.money -= 50;
            DataManager.instance.canStun = true;
        }
    }
    public void BuyUpgradeMaxMoneyIncrease()
    {
        if(cap <= 4)
        {
            DataManager.instance.money -= 100;
            DataManager.instance.maxMoney += 50;
            cap++;
        }
    }
}