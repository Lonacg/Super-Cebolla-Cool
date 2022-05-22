using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private TEST player;

    
    private float firePower;
    private float bulletGravityForce;
    private float bulletFallForce;
    private float initialVelocity;
    private float fireElevation;
    private Vector3 initialDirection;
    private float initialelevation;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<TEST>();

        firePower          = player.firePower;
        bulletFallForce    = player.bulletFallForce;
        bulletGravityForce = player.bulletGravityForce;
        initialDirection   = player.GetOnionModel().forward;
        fireElevation = player.fireElevation;
    }

    void Start()
    {
        initialVelocity  = player.highSpeed;
        rb.AddForce(Vector3.up * 0, ForceMode.Impulse);
    }

    void Update()
    {
        rb.velocity = new Vector3(
            -initialDirection.x * (initialVelocity + firePower),
            rb.velocity.y,
            rb.velocity.z);
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * bulletGravityForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(Vector3.up * fireElevation, ForceMode.Impulse);
    }           
}
























// public class Bullet : MonoBehaviour
// {
//     private float bulletLifeTime;
//     private float bulletMaxBounces;
//     private float bulletFallForce;
//     private float bulletGravityForce;
//     private float fireRate;
//     private float firePower;
//     private float fireElevation;    
//     private float elapsedTime;
//     private float bouncesCounter;

//     private Rigidbody rb;
//     private TEST player;

//     void Awake()
//     {
//         rb = gameObject.GetComponent<Rigidbody>();
//         player = GameObject.FindWithTag("Player").GetComponent<TEST>();

//         bulletLifeTime     = player.bulletLifeTime;
//         bulletMaxBounces   = player.bulletMaxBounces;
//         bulletFallForce    = player.bulletFallForce;
//         bulletGravityForce = player.bulletGravityForce;
//         fireRate           = player.fireRate;
//         firePower          = player.firePower;
//         fireElevation      = player.fireElevation;
//     }

//     void Start()
//     {
//         // rb.velocity = player.GetComponent<Rigidbody>().velocity;
//         rb.velocity = new Vector3(
//             player.GetComponent<Rigidbody>().velocity.x,
//             0,
//             0
//         );

//         rb.AddForce(-player.getOnionModel().transform.forward * firePower);
//         rb.AddForce( player.getOnionModel().transform.up      * fireElevation);
//     }

//     void Update()
//     {
//         if (bulletLifeTime == 0)
//             return;

//         if (elapsedTime < bulletLifeTime)
//             elapsedTime += Time.deltaTime;
//         else
//             Destroy(gameObject);
//     }

//     void FixedUpdate()
//     {
//         if (rb.velocity.y >= 0)
//             rb.AddForce(Vector3.down * bulletGravityForce, ForceMode.Force);

//         if (rb.velocity.y < 0)
//             rb.AddForce(Vector3.down * bulletFallForce, ForceMode.Force);
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet") return;
//         if (bulletMaxBounces <= 0) return;
        
//         if (bouncesCounter < bulletMaxBounces)
//             bouncesCounter++;
//         else
//             Destroy(gameObject);
//     }
// }
