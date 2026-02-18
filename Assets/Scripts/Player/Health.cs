using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;
    public bool isDead = false;

    public void TakeDamage(int damage)
    {
        health = math.clamp(health - damage, 0, maxHealth);

        if (health == 0)
        {
            isDead = true;
        }
    }
}