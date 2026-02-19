using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TaserProjectile : MonoBehaviour
{
    public float bulletSpeed;
    public float freezeduration = 0f;
    public float lifetime = 3f;
    public ParticleSystem vfx;
    public GameObject hitVFX;
    public GameObject explosionVFX;
    public LayerMask obstacleLayer;
    private Vector2 origin;

    void Start()
    {
        origin = transform.position;
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
            GameObject clonedHitVFX = Instantiate(hitVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedHitVFX, freezeduration + 0.5f);
            StopHitVFX(freezeduration, clonedHitVFX.GetComponent<ParticleSystem>());
        }

        else if (guardEnemy)
        {
            guardEnemy.Freeze(freezeduration);
            GameObject clonedHitVFX = Instantiate(hitVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedHitVFX, freezeduration + 0.5f);
            StopHitVFX(freezeduration, clonedHitVFX.GetComponent<ParticleSystem>());
        }

        RaycastHit2D ray = Physics2D.Linecast(origin,collision.transform.position,obstacleLayer);

        if (ray)
        {
            GameObject clonedExplosionVFX = Instantiate(explosionVFX, ray.point, Quaternion.identity);
            Destroy(clonedExplosionVFX, 1);
            Debug.Log("Ray");
        }
        else
        {
            GameObject clonedExplosionVFX = Instantiate(explosionVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedExplosionVFX, 1);
            Debug.Log("NoRay");
        }

        Destroy(gameObject, 0.5f);
        vfx.Stop();
    }

    IEnumerator RemoveEffect(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        vfx.Stop();
    }

    IEnumerator StopHitVFX(float delayTime, ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(delayTime);

        particleSystem.Stop();
    }
}