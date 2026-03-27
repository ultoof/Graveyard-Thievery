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



    void OnTriggerStay2D(Collider2D collision) // Looks if the player is colliding with border. 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warningText.text = "";
        }
    }
}
