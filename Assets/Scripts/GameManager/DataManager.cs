using UnityEngine;

public class DataManager : MonoBehaviour
{
    private bool transportValue;
    public bool TransportValue {get => transportValue; set => transportValue = value;}

    

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
