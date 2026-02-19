using UnityEngine;

public class Taser : MonoBehaviour
{
    public GameObject taserProjectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int ammo = 5;
    public float shootingRate = 0.5f;
    public bool canStun = false;
    public GameObject shootVFX;
    private ParticleSystem shootParticle;

    private void Awake() {
        shootParticle = shootVFX.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        canStun = DataManager.instance.canStun;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canStun == true)
        {
            if(ammo > 0)
            {
                ammo --; 
                shootParticle.Play();

                // Aim
                GameObject taserProjectile = Instantiate(taserProjectilePrefab,transform.position,Quaternion.identity);
                Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - taserProjectile.transform.position;
                diff.Normalize();
                taserProjectile.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
            }
        }
    }

    public void AddAmmo(int count)
    {
        ammo += count;
    }
}