using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    // Properties 
    public int maxHealth = 3;
    public bool isDead = false;
    public GameObject gameOverPanel;

    public void TakeDamage(int damage) // Makes the player take damage 
    {
        if (!isDead)
        {
            DataManager.instance.health = math.clamp(DataManager.instance.health - damage, 0, maxHealth); 

            if (DataManager.instance.health <= 0) // If the player is dead 
            {
                gameOverPanel.SetActive(true); // Spawns the Game over screen 
                isDead = true; 
                DataManager.instance.health = 3;
                DataManager.instance.money = 0;
            }
        }
    }
}