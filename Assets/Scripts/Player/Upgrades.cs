using UnityEngine;

public class Upgrades : MonoBehaviour 
{
    public void BuyUpgradeFlashlight()
    {
        DataManager.instance.TransportValue = true;
    }
}