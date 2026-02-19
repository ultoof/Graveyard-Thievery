using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TaserProjectile : MonoBehaviour 
{
    public float bulletSpeed;
    public float freezeduration = 0f;
    public float lifetime = 3f;
    public ParticleSystem vfx;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifetime + 0.5f);

        RemoveEffect(0.5f);
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

        Destroy(gameObject,0.5f);
        vfx.Stop();
    }

    IEnumerator RemoveEffect(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        vfx.Stop();
    }
}