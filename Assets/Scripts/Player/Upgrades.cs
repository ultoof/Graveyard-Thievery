using Unity.VectorGraphics;
using UnityEngine;

public class Upgrades : MonoBehaviour 
{

    public void BuyUpgradeFlashlight()
    {
        DataManager.instance.canFlash = true;
    }
    public void BuyUpgradeTaser()
    {
        DataManager.instance.canStun = true;
    }
}