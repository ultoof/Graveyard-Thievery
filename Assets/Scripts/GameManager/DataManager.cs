using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> upgradeCap;
    public int difficulty;
    public bool canFlash;
    public bool tutorial;
    public bool canStun;
    public bool soul;
    public int health;
    public float maxMoney;
    public float money;
    public float totalMoney;
    public string position;
}

public class DataManager : MonoBehaviour
{
    // === YOUR ORIGINAL VARIABLES (UNCHANGED) ===
    public List<int> upgradeCap = new List<int>() { 0, 0, 0 };
    public int difficulty = 0;
    public bool canFlash;
    public bool tutorial = false;
    public bool canStun;
    public bool soul = false;
    public int health = 3;
    public float maxMoney;
    public float money;
    public float totalMoney;
    public string position = "Default";

    public static DataManager instance;

    // === NEW: DEFAULT DATA STORAGE ===
    private GameData defaultData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Save default when game starts
            SaveDefault();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // === CREATE A COPY OF CURRENT DATA ===
    GameData GetCurrentData()
    {
        return new GameData
        {
            upgradeCap = new List<int>(upgradeCap), // deep copy list
            difficulty = difficulty,
            canFlash = canFlash,
            tutorial = tutorial,
            canStun = canStun,
            soul = soul,
            health = health,
            maxMoney = maxMoney,
            money = money,
            totalMoney = totalMoney,
            position = position
        };
    }

    // === APPLY DATA BACK INTO MANAGER ===
    void ApplyData(GameData data)
    {
        upgradeCap = new List<int>(data.upgradeCap);

        difficulty = data.difficulty;
        canFlash = data.canFlash;
        tutorial = data.tutorial;
        canStun = data.canStun;
        soul = data.soul;
        health = data.health;
        maxMoney = data.maxMoney;
        money = data.money;
        totalMoney = data.totalMoney;
        position = data.position;

        Debug.Log("Data Reset Applied");

        // Optional: notify other systems
        OnDataReset?.Invoke();
    }

    // SAVE DEFAULT STATE
    public void SaveDefault()
    {
        defaultData = GetCurrentData();
        Debug.Log("Default Data Saved");
    }

    // RESET TO DEFAULT 
    public void ResetToDefault()
    {
        if (defaultData == null)
        {
            Debug.LogWarning("No default data saved!");
            return;
        }

        ApplyData(defaultData);
    }

    // === OPTIONAL: RESET EVENT SYSTEM ===
    public static System.Action OnDataReset;
}