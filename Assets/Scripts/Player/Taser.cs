using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Taser : MonoBehaviour
{
    public GameObject taserProjectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int ammo = 5;
    public float shootingRate = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(ammo > 0)
            {
                //GameObject taserProjectile = Instantiate(taserProjectilePrefab,transform.position, );
                ammo --; 
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
