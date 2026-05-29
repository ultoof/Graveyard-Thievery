using UnityEngine;

public class Taser : MonoBehaviour
{
    // Properties 
    public GameObject taserProjectilePrefab;
    public int ammo = 5;
    public float shootingRate = 0.5f;
    public GameObject shootVFX;
    public GameObject icon;
    public GameObject[] charges;
    public bool hasBeenUsed = false;
    public PlayerController playerController;
    private ParticleSystem shootParticle;

    private void Awake() 
    {
        shootParticle = shootVFX.GetComponent<ParticleSystem>(); // Gets the particle system 

        if (DataManager.instance.canStun)
        {
            icon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && DataManager.instance.canStun == true) // Checks if the player is pressing down T
        {
            if (ammo > 0) // Checks if the player has ammo 
            {
                hasBeenUsed = true;
                charges[5 - ammo].SetActive(false);
                ammo--; // Removes one bullet
                shootParticle.Play(); // Spawns Particle 
                shootVFX.GetComponent<AudioSource>().Play(); // Plays the sound

                // Aim
                GameObject taserProjectile = Instantiate(taserProjectilePrefab, transform.position, Quaternion.identity);
                Vector2 lastDir = playerController.lastDir;
                
                if (lastDir.y > 0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y > 0.05)
                {
                    taserProjectile.transform.rotation = Quaternion.Euler(0, 0, 0f);
                }
                else if (lastDir.y < -0.7 || lastDir.x < 0.1 && lastDir.x > -0.1 && lastDir.y < -0.05)
                {
                    taserProjectile.transform.rotation = Quaternion.Euler(0, 0, 180f);
                }
                else if (lastDir.x > 0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x > 0.05)
                {
                    taserProjectile.transform.rotation = Quaternion.Euler(0, 0, 270f);
                }
                else if (lastDir.x < -0.71 || lastDir.y < 0.1 && lastDir.y > -0.1 && lastDir.x < -0.05)
                {
                    taserProjectile.transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
            }
        }
    }

    public void AddAmmo(int count) // If you pick up ammo 
    {
        ammo += count;
    }

    public void AddIcon()
    {
        icon.SetActive(true);
    }
}