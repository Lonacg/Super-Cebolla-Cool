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
    public GameObject projectile;

    // Variables privadas
    private GameObject onionModel;
    private Rigidbody rb;
    private Vector3 direction;
    private Vector3 HitDirection;
    private float currentSpeed;
    private bool highSpeedKey;
    private bool jumpKey;
    private bool isJumping;
    private float jumpTimeCounter;
    private bool shootKey;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        onionModel= GameObject.Find("OnionModel");
    }

    void Update()
    {
        ListenInputs();
        PlayerMovement();
        PlayerShooting();

        // Error de Unity 
        if (rb.velocity.y == -9.536743e-06f ||
            rb.velocity.y == -2.861023e-05f
        )
            rb.velocity = new Vector3(
                rb.velocity.x,
                0,
                rb.velocity.z
            );
    }

    void FixedUpdate()
    {
        PlayerJumping();
    }

    void OnTriggerStay(Collider collider)
    {   
        CollisionListener(collider);
    }

    void ListenInputs()
    {
        direction = Vector3.zero;

        if (Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            direction = Vector3.right;
            
        if (Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            direction = Vector3.left;

        highSpeedKey = Keyboard.current.shiftKey.isPressed;
        jumpKey  = Keyboard.current.upArrowKey.isPressed;
        shootKey = Keyboard.current.spaceKey.wasPressedThisFrame;
    }
    
    void CollisionListener(Collider collider)
    {
        // 0.7f es para ajustar la cápsula de colisión al movelo, eso no me gusta. el modelo debería ser 0 y nos ahorramos
        // el multiplicarlo por ese valor.     
        Debug.DrawLine( transform.position + Vector3.up * 0.7f, collider.ClosestPoint( transform.position ) );

        Vector3 point = collider.ClosestPoint( transform.position );

        Vector3 dir = (transform.position  + Vector3.up * 0.7f) - point;
        dir.Normalize();
        dir *= -1;  //reemplaza debajo
        // dir = Vector3Int.RoundToInt(dir) * -1;

        // if (rb.velocity.y != 0)
            // dir = new Vector3(dir.x, 0, dir.z);

        Debug.Log(dir);

        HitDirection = dir;
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

        // Error de Unity
        // if (HitDirection.x != 0)
        //     rb.velocity = new Vector3(
        //     0,
        //     rb.velocity.y,
        //     rb.velocity.z
        // );

        if (direction.x != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(-direction, Vector3.up);
            onionModel.transform.rotation = Quaternion.RotateTowards(onionModel.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
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

        if (jumpKey && HitDirection.y < 0 && rb.velocity.y == 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping  = true;
        }
        
        if (rb.velocity.y >= 0)
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Force);

        if (rb.velocity.y < 0)
            rb.AddForce(Vector3.down * fallForce, ForceMode.Force);
    }

    void PlayerShooting()
    {
        // FALTA TERMINAR
        // Un poco guarro (por limpiar)
        if (shootKey)
        {
            float eua = onionModel.transform.rotation.eulerAngles.y;
            if (eua == 90 || eua == 270)
            {
                GameObject bullet = Instantiate(
                projectile,
                new Vector3(
                    transform.localPosition.x + 1 * -Mathf.Sign(onionModel.transform.forward.x),
                    transform.localPosition.y + 0.7f,
                    transform.position.z
                ),
                Quaternion.identity,
                transform
                );
                bullet.GetComponent<Rigidbody>().AddForce(-onionModel.transform.forward * 1200);
            }
        }
    }
}
