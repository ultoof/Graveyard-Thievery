using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    // Properties 
    public int health = 3;
    public int maxHealth = 3;
    public bool isDead = false;
    public GameObject gameOverPanel;

    public void TakeDamage(int damage) // Makes the player take damage 
    {
        health = math.clamp(health - damage, 0, maxHealth); 

        if (health <= 0) // If the player is dead 
        {
            gameOverPanel.SetActive(true); // Spawns the Game over screen 
            Debug.Log("H");

            isDead = true; // He dies 
            Debug.Log("Bro is popped");
        }
    }
}