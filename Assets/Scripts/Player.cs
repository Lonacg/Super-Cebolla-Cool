using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float drag;
    public float directionDrag;
    private float currSpeed;

    private Vector3 lastDirection;
    

    [Header("Salto")]
    public float jumpForce;
    public float gravity;

    public float liftingSpeed;
    public float fallingSpeed; 


    private new Rigidbody rigidbody;

    void Start() {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementControl();
    }

    void FixedUpdate()
    {
        JumpControl();
        CustomGravity();
    }

    private void CustomGravity()
    {
        rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    private void MovementControl()
    {
        Vector3 direction = GetDirection('x');

        bool shiftKey = Keyboard.current.shiftKey.isPressed;

        if (direction.x != 0)
        {
            if (shiftKey)
            {
                // Aceleración
                if (currSpeed < maxSpeed)
                    currSpeed += acceleration * Time.deltaTime;
            }
            else
                currSpeed = speed;

            // Inercia al cambio de dirección
            if (direction != lastDirection)
                currSpeed *= directionDrag; 

            lastDirection = direction;
        }
        else 
            // Inercia al detenerse
            currSpeed *= drag;
    
        transform.Translate(lastDirection * currSpeed * Time.deltaTime);
    }

    private void JumpControl()
    {
        Vector3 direction = GetDirection('y');

        // Borra los comentarios que consideres innecesarios o cámbialos 👍
        // Para la velocidad de caída y la velocidad de salto
        float verticalForce = 1;
        if (direction.y == 1)
            verticalForce = liftingSpeed;
        if (direction.y == -1)
            verticalForce = fallingSpeed;

        // El algoritmo que teníamos en un principio
        if (rigidbody.velocity.y == 0)
        {
            rigidbody.AddForce(
                direction * jumpForce * Time.fixedDeltaTime, 
                ForceMode.Impulse
            );
        }
        else
        {
            if (direction.y != 0)
            {
                // Si quitamos este condicional, la caída tendrá una resistencia al mentener pulsada
                // la tecla up
                if (rigidbody.velocity.y < 0 && direction.y > 0)
                    return;

                // ForceMode.Acceleration quien impulsa la velocidad de caída o salto (llegar más alto)
                rigidbody.AddForce(
                    direction * verticalForce * Time.fixedDeltaTime, ForceMode.Acceleration
                );  
            }
        }

            // if (direction.y == 1)
            // {
            //     if (rigidbody.velocity.y > 0)
            //         rigidbody.AddForce(
            //         Vector3.up * 1500 * Time.fixedDeltaTime, ForceMode.Acceleration
            //     );
            // }
    }

    private Vector3 GetDirection(char axis)
    {
        Vector3 direction = Vector3.zero;

        bool rightPressed= Keyboard.current.rightArrowKey.isPressed;
        bool leftPressed= Keyboard.current.leftArrowKey.isPressed;
        bool upPressed= Keyboard.current.upArrowKey.isPressed;

        switch (axis)
        {
            case 'x':
                if (rightPressed && !leftPressed) direction.x = 1;
                if (leftPressed && !rightPressed) direction.x = -1;
                break;
            case 'y':
                if (upPressed) direction.y =  1;
                break;
        }
        
        return direction;
    }
}