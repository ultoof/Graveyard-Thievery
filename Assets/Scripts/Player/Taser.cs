using UnityEngine;

public class Taser : MonoBehaviour
{
    // Properties 
    public GameObject taserProjectilePrefab;
    public int ammo = 5;
    public float shootingRate = 0.5f;
    public bool canStun = true;
    public GameObject shootVFX;
    private ParticleSystem shootParticle;

    private void Awake() {
        shootParticle = shootVFX.GetComponent<ParticleSystem>(); // Gets the particle system 
    }

    void Start()
    {
        //if (DataManager.instance != null)
        //canStun = DataManager.instance.canStun;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) /*&& canStun == true*/) // Checks if the player is pressing down T
        {
            if (ammo > 0) // Checks if the player has ammo 
            {
                ammo--; // Removes one bullet
                shootParticle.Play(); // Spawns Particle 
                shootVFX.GetComponent<AudioSource>().Play(); // Plays the sound

                // Aim
                GameObject taserProjectile = Instantiate(taserProjectilePrefab, transform.position, Quaternion.identity);
                Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - taserProjectile.transform.position;
                diff.Normalize(); 
                taserProjectile.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
            }
        }
    }

    public void AddAmmo(int count) // If you pick up ammo 
    {
        ammo += count;
    }
}