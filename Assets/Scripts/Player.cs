using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public delegate void PlayerDie(); 
    public static event PlayerDie OnPlayerDie;
    public GameObject hair;

    /*
        Parámetros
    - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    [Header("Parámetros")]
    public int lives;
    public int coins=0;
    public int maxCoins;
    public TextMeshProUGUI coinsLabel;
    public enum State {
        normal,
        big,
        super,
        dead
    }
    public State playerState;

    [Header("Movimiento")]
    public float normalSpeed;
    public float normalSpeedAcceleration;
    public float normalSpeedDeceleration;
   
    [Header("Movimiento Acelerado")]
    public float highSpeed;
    public float highSpeedAcceleration;
    public float highSpeedDeceleration;

    [Header("Rotación del personaje")]
    public float rotationSpeed;

    [Header("Salto")]
    public float jumpTime;
    public float jumpForce;
    public float jumpForceExtra;
    public float fallForce;
    public float gravityForce;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public float bulletFireRate;
    public float bulletLifeTime;
    public int bulletMaxBounces;
    public float bulletVelocity;
    public float bulletInitialElevation;
    public float bulletGravityForce;
    public float bulletBounceForce;


    /*
        Variables de control
    - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    private Rigidbody rb;
    private Transform onionModel;
    private Vector3 MovementDirection;
    private Vector3 HitDirection;

    private float currentSpeed;

    private bool isJumping;
    private float jumpTimeCounter;

    private bool bulletAvailable;
    private float fireRateCounter;
    
    /*
        Inputs
    - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    private bool highSpeedKey;
    private bool jumpKey;
    private bool fireKey;


    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        onionModel = transform.GetChild(0);
        playerState = State.big;
    }

    private void Update()
    {
        InputListener();
        PlayerMovement();
        PlayerRotation();
        coinsLabel.text= $"{coins}";
    }

    private void FixedUpdate()
    {
        PlayerJumping();
        PlayerFiring();
    }

    private void OnCollisionExit(Collision collision)
    {
        HitDirection = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionListener(collision.collider);
        //BlockCollisionListener(collision);
        EnemyCollisionListener(collision);
        //PowerupCollisionListener(collision.collider);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PowerupCollisionListener(collision);
    }

    private void OnTriggerEnter(Collider collider)
    {
        CoinCollisionListener(collider);
        HoleCollisionListener(collider);
        //PowerUpCollisionListener(collider);
        //BombCollisionListener(collider);
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    private void InputListener()
    {
         MovementDirection = Vector3.zero;

        if (Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            MovementDirection = Vector3.right;
        if (Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            MovementDirection = Vector3.left;

        highSpeedKey = Keyboard.current.shiftKey.isPressed;
        jumpKey      = Keyboard.current.upArrowKey.isPressed;
        fireKey      = Keyboard.current.spaceKey.isPressed;
    }

    private void CollisionListener(Collider collider)
    {
        Vector3 point = collider.ClosestPointOnBounds( transform.position );
        Vector3 dir = (transform.position - point).normalized * -1;
        HitDirection += dir;
        HitDirection.Normalize();
        HitDirection = Vector3Int.RoundToInt(HitDirection);

        // DEBUG
        Debug.DrawLine( transform.position, collider.ClosestPointOnBounds( transform.position ), Color.red );
    }

    private void PlayerRotation()
    {
        if (MovementDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(-MovementDirection, Vector3.up);
            onionModel.rotation = Quaternion.RotateTowards(onionModel.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (
                onionModel.rotation.eulerAngles.y != 90 &&
                onionModel.rotation.eulerAngles.y != 270
            )
            {
                if (Mathf.Abs(currentSpeed) > 0)
                {
                    if (currentSpeed > normalSpeed)
                        currentSpeed += normalSpeedDeceleration * -Mathf.Sign(currentSpeed);
                    if (currentSpeed > highSpeed)
                        currentSpeed += highSpeedDeceleration * -Mathf.Sign(currentSpeed);
                }
                else
                    currentSpeed = 0;

                rb.velocity = new Vector3(currentSpeed, rb.velocity.y, rb.velocity.z);
            }
        }
    }

    private void PlayerMovement()
    {
        if (!highSpeedKey)
            MovementDynamic(0, normalSpeed, normalSpeedAcceleration, normalSpeedDeceleration);
        else
            MovementDynamic(0, highSpeed, highSpeedAcceleration, highSpeedDeceleration);

        if (HitDirection.x < 0 && currentSpeed < 0)
            currentSpeed = 0;
        if (HitDirection.x > 0 && currentSpeed > 0)
            currentSpeed = 0;

        rb.velocity = new Vector3(currentSpeed, rb.velocity.y, rb.velocity.z);
    }

    private void MovementDynamic(
        float minSpeed,
        float maxSpeed,
        float acceleration,
        float deceleration
    )
    {
        if (MovementDirection != Vector3.zero)
        {
            if (Mathf.Abs(currentSpeed) <= maxSpeed)
            {
                currentSpeed += acceleration * MovementDirection.x;
                currentSpeed = Mathf.Abs(currentSpeed) < maxSpeed ? currentSpeed : maxSpeed * MovementDirection.x;
            }
            else
                currentSpeed -= acceleration * Mathf.Sign(currentSpeed);
        }
        else
        {
            if ( Mathf.Abs(currentSpeed) > minSpeed)
                currentSpeed += deceleration * -Mathf.Sign(currentSpeed);
            if (Mathf.Abs(currentSpeed) < acceleration)
                currentSpeed = 0;
        }
    }
    
    private void PlayerJumping()
    {
        if (isJumping)
        {
            if (jumpKey)
            {
                if (jumpTimeCounter < jumpTime)
                    jumpTimeCounter += Time.deltaTime;
                else
                {
                    jumpTimeCounter = 0;
                    isJumping = false;
                }

                if (rb.velocity.y >= 0)
                    rb.AddForce(Vector3.up * jumpForceExtra, ForceMode.Acceleration);
            }
        }

        if (jumpKey && HitDirection.y < 0 && rb.velocity.y <= 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping  = true;
        }
        
        if (rb.velocity.y >= 0)
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Force);

        if (rb.velocity.y < 0)
            rb.AddForce(Vector3.down * fallForce, ForceMode.Force);
    }

    private void PlayerFiring()
    {
        if (playerState != State.super )
            return;

        if (!bulletAvailable)
        {
            if (fireRateCounter < bulletFireRate)
                fireRateCounter += Time.deltaTime;
            else
                bulletAvailable = true;
        }
        else
        {
            if (fireKey)
            {
                if (onionModel.rotation.eulerAngles.y == 90 || onionModel.rotation.eulerAngles.y == 270)
                {
                    Vector3 respawn = new Vector3(transform.position.x - onionModel.transform.forward.x,transform.position.y,transform.position.z);
                    GameObject bullet = Instantiate(bulletPrefab, respawn, Quaternion.identity);
                    Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
                }
                bulletAvailable = false;
                fireRateCounter = 0;
            }
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    /*void BombCollisionListener(Collider collider)
    {
        if (collider.CompareTag("Bomb"))
        {
            if (playerState == State.normal)
                PlayerIsDead("Bomb");
            if (playerState == State.big)
                PlayerIsNormal(collider.gameObject);
            if (playerState == State.super)
                PlayerIsBig(true);
        }
    }

    void BlockCollisionListener(Collision collision)
    {
        if (collision.transform.tag == "Block")
        {
            if (HitDirection.y > 0 && HitDirection.x == 0)
                collision.gameObject.GetComponent<OLDBlock>().ChangeBlockState(gameObject);
        }
    }*/

    void CoinCollisionListener(Collider collider)
    {
        if (collider.transform.tag == "Coin")
        {
            coins += 1;
            if (coins >= maxCoins)
            {
                lives += 1;
                coins = 0;
            }
        }
    }

    void HoleCollisionListener(Collider collider)
    {
        if (collider.transform.tag == "Hole")
            PlayerIsDead();
    }

    void EnemyCollisionListener(Collision collision)
    {
        if (collision.transform.tag != "Enemy")
            return;

        if (HitDirection.x != 0 || HitDirection.y >= 0)
        {
            if (playerState == State.normal)
                PlayerIsDead();

            if (playerState == State.big)

                PlayerIsNormal(collision.gameObject);
                

            if (playerState == State.super)
                PlayerIsBig(collision.gameObject);

            return;
        }

        if (HitDirection.y < 0)
            rb.AddForce(Vector3.up * jumpForce / 2.5f, ForceMode.Impulse);
    }

    /*void PowerUpCollisionListener(Collider collider)
    {
        if (collider.CompareTag("Poop")) //caca
        {
            Destroy(collider.gameObject);
            if (playerState != State.super)
                PlayerIsSuper();
        }
    }*/

    void PowerupCollisionListener(Collision collision)
    {
        if (collision.transform.tag=="WaterDrop") //gota de agua
        {
            Destroy(collision.gameObject);
            if (playerState == State.normal)
                PlayerIsBig();
        }
        if (collision.transform.tag=="Poop") //caca
        {
            Destroy(collision.gameObject);
            if (playerState != State.super)
                PlayerIsSuper();
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    void PlayerIsDead()
    {
        if (OnPlayerDie!=null)
            OnPlayerDie();
    }

    
    /*void PlayerIsDead(string cause)
    {
        lives -= 1;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            playerState = State.dead;
            gameObject.SetActive(false);
        }
        else
        {
            switch (cause)
            {
                case "Hole":
                    StartCoroutine(PlayerRespawn(0f));
                    break;
                case "Enemy":
                    StartCoroutine(PlayerRespawn(0));
                    break;
                case "Bomb":
                    StartCoroutine(PlayerRespawn(0));
                    break;
            }
        }
    }*/

    void PlayerIsNormal(GameObject enemy)
    {
        playerState = State.normal;
        transform.localScale = new Vector3(1, 0.5f, 1);
        StartCoroutine( Invencible(enemy) );
        //Debug.Log("El jugador ha vuelto al estado normal...");
    }

    //void PlayerIsBig(bool returning = false)
    void PlayerIsBig(GameObject enemy=null)
    {
        hair.SetActive(false);
        playerState = State.big;
        transform.localScale = Vector3.one;
        if(enemy!=null)
            StartCoroutine( Invencible(enemy) );

        // Parameters (if there are)
        //bulletFireRate = 0.1f;

        /*if (returning)
            Debug.Log("El jugador ha vuelto al modo grande...");
        else
            Debug.Log("El jugador evoluciona al modo grande...");*/
    }

    void PlayerIsSuper()
    {
        hair.SetActive(true);

        playerState = State.super;
        transform.localScale = Vector3.one;

        // Parameters (if there are)
        //bulletFireRate = 0.2f;

        //Debug.Log("El jugador evoluciona al modo abono...");
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    /*IEnumerator PlayerRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        playerState = State.normal;
        gameObject.transform.position = new Vector3(-5.5f, 4, 0);
    }*/

    IEnumerator Invencible(GameObject enemy)
    {
        float counter = 0;

        while (counter < 3)
        {
            if(enemy==null) break;
            Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>());
            counter += Time.deltaTime;
            yield return null;
        }
        if(enemy==null) yield break;
        Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - */
    public Transform GetOnionModel() { return onionModel; }
}
