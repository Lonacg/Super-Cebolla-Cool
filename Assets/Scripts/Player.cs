using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float normalSpeed;
    public float normalSpeedAcceleration;
    public float normalSpeedDeceleration;

    [Header("Movimiento Acelerado")]
    public float highSpeed;
    public float highSpeedAcceleration;
    public float highSpeedDeceleration;

    [Header("Salto")]
    public float jumpTime;
    public float jumpForce;
    public float jumpForceExtend;
    public float fallForce;
    public float gravityForce;

    [Header("Misc")]
    public float rotationSpeed;

    // Variables privadas
    private Rigidbody rb;
    private Vector3 direction;
    private float currentSpeed;
    private bool highSpeedKey;
    private bool jumpKey;
    private bool isGrounded;
    private bool isWall;
    private bool isJumping;
    private float jumpTimeCounter;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ListenInputs();
        // CollisionListener();
        PlayerMovement();
    }

    void FixedUpdate()
    {
        PlayerJumping();
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        isWall = other.transform.tag == "Wall" ? true : false;
        isGrounded = true;
    }

    void ListenInputs()
    {
        direction = Vector3.zero;

        if (Keyboard.current.rightArrowKey.isPressed)
            direction = Vector3.right;
        if (Keyboard.current.leftArrowKey.isPressed)
            direction = Vector3.left;

        highSpeedKey = Keyboard.current.shiftKey.isPressed;
        jumpKey = Keyboard.current.upArrowKey.isPressed;
    }
    
    void CollisionListener()
    {
        // PENDIENTE DE TERMINAR 

        RaycastHit hit;
        if ( Physics.Raycast( transform.position, transform.position + (Vector3.down * 2), out hit ) )
        {
            Debug.DrawLine( transform.position, transform.position + (Vector3.down * 2), Color.white );
        }
        else
        {
            Debug.DrawLine( transform.position, transform.position + (Vector3.down * 2), Color.red );
        }
    }
    
    private void PlayerMovement()
    {
        if (highSpeedKey)
            MovementDynamic(
                0, highSpeed, highSpeedAcceleration, highSpeedDeceleration
            );
        else
        {
            // Desaceleración de alta velocidad a velocidad normal (pero da un bug que se trata abajo)
            if (Mathf.Abs(currentSpeed) > normalSpeed + normalSpeedAcceleration)
                currentSpeed -= normalSpeedAcceleration * direction.x;
            // Si da igual la desaceleración, dejar únicamente lo de abajo y borrar el resto
            else
                MovementDynamic(
                    0, normalSpeed, normalSpeedAcceleration, normalSpeedDeceleration
                );
        }

        // Corrección de un bug que hace que se deslice hasta el infinito (funciona, pero quita una característica)
        if (!highSpeedKey && direction.x == 0 && Mathf.Abs(currentSpeed) > normalSpeed + normalSpeedAcceleration)
            currentSpeed = 0;
        
        // PENDIENTE DE TERMINAR
        if (isWall && !isGrounded) currentSpeed = 0;
        // transform.Translate( transform.right * currentSpeed * Time.deltaTime );

        if (direction.x != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        rb.velocity = new Vector3(
            currentSpeed,
            rb.velocity.y,
            rb.velocity.z
        );
    }

    private void MovementDynamic(
        float minSpeed,
        float maxSpeed,
        float acceleration,
        float deceleration
    )
    {
        if (direction.x != 0)
        {
            currentSpeed += acceleration * direction.x;
            currentSpeed = Mathf.Abs(currentSpeed) < maxSpeed ? currentSpeed : maxSpeed * direction.x;
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
                    rb.AddForce(Vector3.up * jumpForceExtend, ForceMode.Acceleration);
            }
        }

        if (jumpKey && isGrounded)
            //Debug.Log("salta");

        if (jumpKey && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping  = true;
            isGrounded = false;
        }
        
        if (rb.velocity.y >= 0)
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Force);

        if (rb.velocity.y < 0)
            rb.AddForce(Vector3.down * fallForce, ForceMode.Force);
    }
}
