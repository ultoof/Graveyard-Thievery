using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;
    public bool isDead = false;
    public GameObject gameOverPanel;

    public void TakeDamage(int damage)
    {
        health = math.clamp(health - damage, 0, maxHealth);

        if (health <= 0)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("H");

            isDead = true;
            Debug.Log("Bro is popped");
        }
    }
}