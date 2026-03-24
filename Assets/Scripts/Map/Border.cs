using UnityEngine;

public class Border : MonoBehaviour
{
    public int difficulty;
    private int playerDifficulty = DataManager.instance.difficulty;

    void Awake()
    {
        if (playerDifficulty >= difficulty)
        {
            Destroy(gameObject);  
            Debug.Log("VIGGO LOVES FEMBOYS = TRUE");
        }
    }
}
