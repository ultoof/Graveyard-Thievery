using System.Numerics;
using UnityEngine;

public class FireFlies : MonoBehaviour 
{
    public Transform fireFly;
    public float baseSpeed;
    public float aimlessStrength; //How chaotic they move
    public float directionChangeSpeed; //How often they change direcztions

    private UnityEngine.Vector2 currentDirection;
    private Rigidbody2D rb;

    void Start()
    {
        currentDirection = Random.insideUnitCircle.normalized;
    } 
    void Awake()
    {
        fireFly = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    //Smoothly change directions 
    void FixedUpdate()
    {
        //Chooses a random direction 360(Circle)
        UnityEngine.Vector2 randomOffset = Random.insideUnitCircle * aimlessStrength;
        currentDirection = UnityEngine.Vector2.Lerp(currentDirection, randomOffset, directionChangeSpeed*Time.fixedDeltaTime);

        rb.AddForce(currentDirection.normalized * baseSpeed);
    }

}
