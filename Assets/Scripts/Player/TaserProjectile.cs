using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]

public class TaserProjectile : MonoBehaviour 
{
    public float bulletSpeed;
    public float freezeduration = 0f;
    public float lifetime = 3f;
    public float knockback = 0f;

    private NavMeshAgent frozenAgent;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        } 

        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        GuardEnemy guardEnemy = collision.gameObject.GetComponentInParent<GuardEnemy>();

        if (enemy)
        {
            enemy.Freeze(freezeduration);
        }

        else if(guardEnemy)
        {
            guardEnemy.Freeze(freezeduration);
        }
        else
        {
            
        }
        Destroy(gameObject);
        
    }

}

