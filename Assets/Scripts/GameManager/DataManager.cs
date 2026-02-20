using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    public bool canFlash;
    public bool canStun;
    public float maxMoney;
    

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
