using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int difficulty = 0;
    public bool canFlash;
    public bool tutorial = false;
    public bool canStun;
    public bool soul = false;
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
        else
        {
            Destroy(gameObject);
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
