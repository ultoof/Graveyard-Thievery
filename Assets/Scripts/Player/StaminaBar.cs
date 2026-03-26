using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Properties 
    public Image staminaBar;
    public PlayerController playerController;

    private void Update()
    {
        staminaBar.fillAmount = playerController.Stamina / 1000f; // Makes the bar shrink when you run and use stamina.
    }
}