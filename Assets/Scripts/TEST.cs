using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [Header("Movimiento normal")]
    public float normalSpeed;
    public float normalSpeedAcceleration;
    public float normalSpeedDeceleration;
    public float normalSpeedCDR; // CDR = Change Direction Resistance

    [Header("Movimiento acelerado")]
    public float maxSpeed;
    public float maxSpeedAcceleration;
    public float maxSpeedDeceleration;
    public float maxSpeedCDR;

    
    // Private variables
    private Rigidbody rb;
    private float direction;
    private float currSpeed;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = MovementDirection();
    }

    void FixedUpdate()
    {
        playerMovement();
    }

    float MovementDirection()
    {
        float direction = 0;

        if (Keyboard.current.rightArrowKey.isPressed)
            direction = 1;
        if (Keyboard.current.leftArrowKey.isPressed)
            direction = -1;

        return direction;
    }

    // void playerMovement()
    // {
    //     if (direction > 0)
    //     {
    //         if (currSpeed < normalSpeed)
    //         {
    //             currSpeed += normalSpeedAcceleration;
    //             if (currSpeed < 0)
    //                 currSpeed += normalSpeedAcceleration * normalSpeedCDR;
    //         }            
    //     }
    //     if (direction < 0)
    //     {
    //         if (currSpeed > -normalSpeed)
    //         {
    //             currSpeed -= normalSpeedAcceleration;
    //             if (currSpeed > 0)
    //                 currSpeed += normalSpeedAcceleration * normalSpeedCDR;
    //         }
    //     }
    //     if ( direction == 0 )
    //     {
    //         if (currSpeed > 0)
    //             currSpeed -= normalSpeedDeceleration;
    //         if (currSpeed < 0)
    //             currSpeed += normalSpeedDeceleration;
    //         if (Mathf.Abs(currSpeed) < normalSpeedAcceleration)
    //             currSpeed = 0;
    //     }


    //     rb.velocity = new Vector3(
    //         currSpeed,
    //         rb.velocity.y,
    //         rb.velocity.z
    //     );


    // }

    void playerMovement()
    {
        // if (direction != 0)
        // {
        //     float drag = 1;
        //     if ( Mathf.Abs(currSpeed) > 0 && Mathf.Sign(currSpeed) != direction )
        //         drag = changeDirectionDrag;

        //     currSpeed += normalSpeedAcceleration * direction * drag;
        //     currSpeed = Mathf.Abs(currSpeed) < normalSpeed ? currSpeed : normalSpeed * direction;            
        // }
        
        // if (direction == 0)
        // {
        //     if ( Mathf.Abs(currSpeed) > 0 )
        //         currSpeed += normalSpeedDeceleration * -Mathf.Sign(currSpeed);
        //     // 0.1f valor mínimo para cambiar a 0 y detenerse
        //     if (Mathf.Abs(currSpeed) < 0.1f)
        //         currSpeed = 0;
        // }

        bool shiftKey = Keyboard.current.shiftKey.isPressed;

        Vector3 playerMovement = Vector3.zero;

        if ( !shiftKey )
        {
            playerMovement.x = movementAcceleration( 
                0, 
                normalSpeed, 
                normalSpeedAcceleration, 
                normalSpeedDeceleration, 
                normalSpeedCDR
            );
        }
        else
        {
            playerMovement.x = movementAcceleration(
                0,
                maxSpeed,
                maxSpeedAcceleration,
                maxSpeedDeceleration,
                maxSpeedCDR
            );
        }

        rb.velocity = new Vector3(
            playerMovement.x,
            rb.velocity.y,
            rb.velocity.z
        );
    }

    float movementAcceleration(
        float minSpeed, 
        float maxSpeed, 
        float acceleration, 
        float deceleration, 
        float changeDirectionResistance)
    {
        if (direction != 0)
        {
            float drag = 1;
            // if ( Mathf.Abs(currSpeed) > minSpeed && Mathf.Sign(currSpeed) != direction )
            //     drag = changeDirectionResistance;
            currSpeed += acceleration * direction * drag;
            currSpeed = Mathf.Abs(currSpeed) < maxSpeed ? currSpeed : maxSpeed * direction;            
        }
        else
        {
            if ( Mathf.Abs(currSpeed) > minSpeed)
                currSpeed += deceleration * -Mathf.Sign(currSpeed);
            if (Mathf.Abs(currSpeed) < acceleration)
                currSpeed = 0;
        }

        return currSpeed;
    }

    // float aaa()
    // {
    //      Si se mueve a laderecha
    //     if (direction > 0)
    //     {
    //         if (numeroMagico < 5)
    //             numeroMagico += 0.1f;
    //     }
    //      Si se meuve a la izuqierda
    //     if (direction < 0)
    //     {
    //         if (numeroMagico > -5)
    //             numeroMagico -= 0.1f;
    //     }

    //      Si deja de moverse
    //     if ( direction == 0 )
    //     {
    //         if (numeroMagico > 0)
    //             numeroMagico -= 0.1f;

    //         if (numeroMagico < 0)
    //             numeroMagico += 0.1f;

    //          corrección si no es 0 el valor de descanso (lo fuerza a 0)
    //         if (Mathf.Abs(numeroMagico) < 0.1f)
    //             numeroMagico = 0;
    //     }

    //     return numeroMagico;
    // }

}
