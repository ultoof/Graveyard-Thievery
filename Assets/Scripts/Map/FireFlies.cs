using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//Note:  Unity some times fights with system over Vectors so its best to indicate what library you want to use 
//For those who might snoop dont remove the UnityEngine behind any vector
[RequireComponent(typeof(Rigidbody2D))]
public class FireFlies : MonoBehaviour 
{ 
    //Properties :
    [Header("Movement")]
    public Transform fireFly;
    public float baseSpeed; //How fast they travel
    public float aimlessStrength; //How chaotic they move
    public float directionChangeSpeed; //How often they change direcztions

    [Header("Return To Sender")]
    public float maxDistance;
    public float returnToPapa;


    private UnityEngine.Vector2 currentDirection;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 origin; //Wher i originally set the "FireFLy"

    Light2D light2D;
    
    [Header("Intensity")]
    public float baseIntensity = 1.2f;
    public float intensityVariance = 0.4f;
    public float intensitySpeed = 3f;

    [Header("Range")]
    public float baseRange = 1.5f;
    public float rangeVariance = 0.3f;
    public float rangeSpeed = 2.5f;

    void Start()
    {
        currentDirection = Random.insideUnitCircle.normalized;
    } 
    void Awake()
    {
        //We are assigning the properties from the gameobjects. Its basically : gameobject.GetComponent
        fireFly = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        light2D = GetComponent<Light2D>();
        origin = rb.position;
    }
    
    //Smoothly change directions 
    void FixedUpdate()
    {
        //Chooses a random direction 360(Circle)
        // This code is mostly from a tutorial
        UnityEngine.Vector2 randomOffset = Random.insideUnitCircle * aimlessStrength;
        currentDirection = UnityEngine.Vector2.Lerp(currentDirection, randomOffset, directionChangeSpeed*Time.fixedDeltaTime);

        rb.AddForce(currentDirection.normalized * baseSpeed);

        //Pull Back To Origin if the firefly strays too far : 
        UnityEngine.Vector2 toOrigin = origin - rb.position; // Holy math class : When u subtract 2 vectors u get a new vector. Thos vector points from curremt position to origin #Ulf carry
        if(toOrigin.magnitude > maxDistance) // If the new vector is greater than the max distance
        {
            rb.AddForce(toOrigin.normalized * returnToPapa); //We add force to origin and the speed of return to papa. 
        } 
    }

    

    void Update()
    {
        float intensity = baseIntensity + Mathf.PerlinNoise(Time.time * intensitySpeed, 0f) * intensityVariance; // This is the main flicker. For those who dont know math.perlin noise returns a smooth variable from 1 - 10
        float range = baseRange + Mathf.PerlinNoise(0f, Time.time * rangeSpeed) * rangeVariance;

        light2D.intensity = intensity;
        light2D.pointLightOuterRadius = range;
    }

    // THe reason we dont use random.range is bc perlinNoise has cleaner transitions between numbers. random. range would be chaotic asf
    //Time.time * intensitySpeed : This is just transitioning over time. The faster intensity speed is the faster it moves   through noise and how much it flickers
    // This logic applies to the flickering range to0

    //ALL MY HOMIES HATE VECTORS 
}
