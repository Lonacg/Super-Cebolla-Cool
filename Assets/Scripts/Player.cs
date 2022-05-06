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

    // Variables nuevas
    private Transform playerModel;
    private Vector3 playerModel_oldForward;


    void Start() {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0).transform;

        playerModel_oldForward = playerModel.forward;
    }

    void Update()
    {  
    }

    void FixedUpdate()
    {
        MovementControl();
        JumpControl();
        CustomGravity();
    }

    private void CustomGravity()
    {
        rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    private void MovementControl()
    {
        bool shiftKey = Keyboard.current.shiftKey.isPressed;
        Vector3 direction = GetDirection('x');

        if (direction.magnitude != 0)
            playerModel_oldForward = direction;

        if (direction.x != 0)
        {
            if (shiftKey)
            {
                if (currSpeed < maxSpeed)
                    currSpeed += acceleration * Time.deltaTime;
            }
            else
                currSpeed = speed;

            if (direction != lastDirection)
                currSpeed *= directionDrag;

            lastDirection = direction;
        }
        else 
            currSpeed *= drag;
    

        transform.Translate(lastDirection * currSpeed * Time.deltaTime);
        UpdatePlayerModelRotation();
    }

    private void UpdatePlayerModelRotation()
    {
        playerModel.forward = Vector3.Slerp(playerModel.forward, playerModel_oldForward.normalized, Time.deltaTime * 12);
    }

    private void JumpControl()
    {
        Vector3 direction = GetDirection('y');

        float verticalForce = 1;
        if (direction.y == 1)
            verticalForce = liftingSpeed;
        if (direction.y == -1)
            verticalForce = fallingSpeed;

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
                // Si quitamos este condicional, la caída tendrá una resistencia al mentener pulsada la tecla up
                if (rigidbody.velocity.y < 0 && direction.y > 0)
                    return;

                rigidbody.AddForce(
                    direction * verticalForce * Time.fixedDeltaTime, ForceMode.Acceleration
                );  
            }
        }
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