using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image staminaBar;
    public PlayerController playerController;

    private void Update()
    {
        staminaBar.fillAmount = playerController.Stamina / 1000f;
    }
}