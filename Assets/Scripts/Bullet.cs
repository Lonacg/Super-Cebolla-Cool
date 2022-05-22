using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private TEST player;

    private float lifeTime;
    private int maxBounces;
    private float gravityForce;
    private float bounceForce;
    private float velocity;
    private float initialElevation;
    private float initialVelocity;
    private Vector3 initialDirection;

    private float elapsedTime;
    private int bouncesCounter;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<TEST>();

        lifeTime         = player.bulletLifeTime;
        maxBounces       = player.bulletMaxBounces;
        gravityForce     = player.bulletGravityForce;
        bounceForce      = player.bulletBounceForce;
        velocity         = player.bulletVelocity;
        initialElevation = player.bulletInitialElevation;
        initialVelocity  = player.highSpeed;
        initialDirection = player.GetOnionModel().forward;
    }

    void Start()
    {
        rb.AddForce(Vector3.up * initialElevation, ForceMode.Impulse);
    }

    void Update()
    {
        BulletDuration();
        BulletDisplacement();
        Gravity();      
    }

    void OnCollisionEnter(Collision collision)
    {
        // IGNORE
        if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet") return;

        // BOUNCES COUNTER
        BulletBounces();

        // BOUNCE FORCE
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    } 

    private void Gravity()
    {
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Impulse);
    }

    private void BulletDisplacement()
    {
        if (velocity != 0)
            rb.velocity = new Vector3(
                -initialDirection.x * (initialVelocity + velocity),
                rb.velocity.y,
                rb.velocity.z);
    }

    private void BulletDuration()
    {
        if (lifeTime == 0) return;

        if (elapsedTime < lifeTime)
            elapsedTime += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void BulletBounces()
    {
        if (maxBounces <= 0) return;
        
        if (bouncesCounter < maxBounces - 1)
            bouncesCounter++;
        else
            Destroy(gameObject);
    }
}