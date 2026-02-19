using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    public bool canFlash;
    public bool canStun;
    public bool doubleJumpUnlocked = false;
    

    public static DataManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
