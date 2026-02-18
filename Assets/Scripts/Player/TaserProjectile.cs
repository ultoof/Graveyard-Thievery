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
    private float placeholderValue = 0;
    private float placeholderValueA = 0;

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

        NavMeshAgent nav  = collision.gameObject.GetComponentInParent<NavMeshAgent>();

        if (nav)
        {
            nav.isStopped = true;
            StartCoroutine(DelayAction(3f, nav));
        }
        else
        {
            
        }
        Destroy(gameObject);
    }

     IEnumerator DelayAction (float delayTime,NavMeshAgent nav)
    {
        yield return new WaitForSeconds(delayTime);
        nav.isStopped = false;
        Debug.Log("Its working");

    }
}

/*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GameObject().CompareTag("Player"))
        {
            return;
        }

        Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        
        if (rb)
        {
            Debug.Log("BROSEFSKI");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(DelayAction(freezeduration,rb));
        }
        else //if it hits another collider
        {
            Debug.Log("FAHHHH "); 
        }
        Destroy(gameObject);


    IEnumerator DelayAction (float delayTime,Rigidbody2D rb)
    {
        yield return new WaitForSeconds(delayTime);

        rb.constraints = RigidbodyConstraints2D.None;
    }

    }
}

*/