using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    public bool canFlash;
    public bool canStun;
    public float maxMoney;
    public float money;

    public static DataManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

        /*
        L = GameObject.FindGameObjectsWithTag("");
        foreach(GameObject l in L)
        {
            if(l == GameObject.Find(""))
        }
        */
    
}
