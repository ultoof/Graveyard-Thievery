using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]

public class TaserProjectile : MonoBehaviour 
{
    public float bulletSpeed;
    public float lifetime = 3f;
    public float knockback = 0f;
    private float placeholderValue;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GameObject().CompareTag("Player"))
        {
            return;
        }

        NavMeshAgent navMeshAgent = collision.GameObject().GetComponent<NavMeshAgent>();

        if (navMeshAgent)
        {
            Debug.Log("Dab");
            placeholderValue = navMeshAgent.speed;
            navMeshAgent.speed = 0;
            
            StartCoroutine(DelayAction(3f,navMeshAgent));
            if(knockback > 0)
            {
                Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
                if(rb != null)
                {
                    rb.AddForce(GetComponent<Rigidbody2D>().linearVelocity.normalized * knockback);
                }
            }

        }
        //If it hits another type of collider
        else
        {
            
        }

        Destroy(gameObject);
    }


    IEnumerator DelayAction (float delayTime,NavMeshAgent navMeshAgent)
    {
        yield return new WaitForSeconds(delayTime);

        navMeshAgent.speed = placeholderValue;
    }


}