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
    private float currSpeed;

    [Header("Salto")]
    public float jumpForce;
    public float gravity;

    private new Rigidbody rigidbody;

    void Start() {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = MovementControl();

        accelerationControl();
        
        transform.Translate(
            direction * currSpeed * Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        JumpControl();
        SetGravity();
    }

    private void SetGravity()
    {
        rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    private Vector3 MovementControl()
    {
        Vector3 axis = Vector3.zero;   

        // Control del movimiento del jugador en el eje X (izquierda a derecha)
        if (Keyboard.current.leftArrowKey.isPressed)  
            axis.x = -1;
        if (Keyboard.current.rightArrowKey.isPressed) 
            axis.x =  1;
       
        return axis;
    }

    private void JumpControl()
    {
        // Control del salto del jugador en el eje Y
        if (Keyboard.current.upArrowKey.isPressed) 
        {
            // Este if se asegura que solo podamos pulsar la tecla de salto una vez hasta que caiga al suelo
            // sin importar la altura de la superficie
            if (rigidbody.velocity.y == 0)
                rigidbody.AddForce(
                    Vector3.up * jumpForce * Time.fixedDeltaTime, 
                    ForceMode.Impulse
                );
        }
    }

    private void accelerationControl()
    {
        /*
            Funciona pero no se ve tan... mario
            Podríamos inentar que al cambiar de dirección la aceleración sea 0
            o hacer que tenga alguna inercia
        */

        // Control de la aceleración al pulsar la tecla Shift
        if (Keyboard.current.shiftKey.isPressed)
        {
            if (currSpeed < maxSpeed)
                currSpeed += acceleration * Time.deltaTime;
        }
        else
            currSpeed = speed;
    }
}
