using System;
using TMPro;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Properties
    public int difficulty;
    private int playerDifficulty = DataManager.instance.difficulty;
    public TextMeshProUGUI warningText;

    void Awake()
    {
        if (playerDifficulty >= difficulty) // Checks if the player has bought enough upgrades for it to work
        {
            Destroy(gameObject);  
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warningText.text = "You are not worthy to pass this shadow yet...";
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warningText.text = "";
        } 
    }
}
