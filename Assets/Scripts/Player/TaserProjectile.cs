using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TaserProjectile : MonoBehaviour
{
    // Properties 
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
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse); // Forces the projectile forwards
        Destroy(gameObject, lifetime + 0.5f); // Destroys it after a while
        RemoveEffect(0.5f); // Removes the effect 
    }

    void OnTriggerEnter2D(Collider2D collision) // If the player in range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>(); // Gets the enemys collider
        GuardEnemy guardEnemy = collision.gameObject.GetComponentInParent<GuardEnemy>();

        if (enemy) // If you shoot the normal enemy
        {
            enemy.Freeze(freezeduration); // Freezes him for a bit 
            GameObject clonedHitVFX = Instantiate(hitVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedHitVFX, freezeduration + 0.5f);
            StopHitVFX(freezeduration, clonedHitVFX.GetComponent<ParticleSystem>());
        }

        else if (guardEnemy) // If you shoot the guard
        {
            guardEnemy.Freeze(freezeduration); // Freezes him for this time
            GameObject clonedHitVFX = Instantiate(hitVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedHitVFX, freezeduration + 0.5f); // Destroys the vfx
            StopHitVFX(freezeduration, clonedHitVFX.GetComponent<ParticleSystem>());
        }

        RaycastHit2D ray = Physics2D.Linecast(origin,collision.transform.position,obstacleLayer);

        if (ray)
        {
            GameObject clonedExplosionVFX = Instantiate(explosionVFX, ray.point, Quaternion.identity);
            Destroy(clonedExplosionVFX, 4);
        }
        else
        {
            GameObject clonedExplosionVFX = Instantiate(explosionVFX, collision.transform.position, Quaternion.identity);
            Destroy(clonedExplosionVFX, 4);
        }

        Destroy(gameObject, 4f);
        vfx.Stop();
    }

    IEnumerator RemoveEffect(float delayTime) // Removes the effect 
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