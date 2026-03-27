using UnityEngine;
using UnityEngine.UI;

public class HeartsAnimator : MonoBehaviour
{
    // Properties 
    public Image[] hearts;
    public Health healthScript;
    private int nrOfHearts;

    void Start()
    {
        nrOfHearts = hearts.Length; // How many hearts you start with 
    }

    void Update()
    {
        float frame = (float)healthScript.health / (float)healthScript.maxHealth;
        DisplayHearts(Mathf.RoundToInt(frame * nrOfHearts)); // It will remove hearts when taking damage 
    }

    void DisplayHearts(int count) // It shows the hearts
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < count ? true : false;
        }
    }
}
