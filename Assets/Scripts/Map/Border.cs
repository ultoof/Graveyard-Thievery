using System;
using TMPro;
using UnityEngine;

public class Border : MonoBehaviour
{
    public int difficulty;
    private int playerDifficulty = DataManager.instance.difficulty;
    public TextMeshProUGUI warningText;

    void Awake()
    {
        if (playerDifficulty >= difficulty)
        {
            Destroy(gameObject);  
            Debug.Log("VIGGO LOVES FEMBOYS = TRUE");
        }
    }

    //ts is just if we want a text to popup when the player arrives at the border
    /*
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warningText.text = "";
        }
    }
    */
}
