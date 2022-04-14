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
    private float currSpeed;

    private Vector3 lastDirection;
    

    [Header("Salto")]
    public float jumpForce;
    public float gravity;

    private new Rigidbody rigidbody;

    void Start() {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {   
        MovementControl();
        accelerationControl();
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


    private void MovementControl()
    {

        Vector3 direction = Vector3.zero;   

        // Control del movimiento del jugador en el eje X (izquierda a derecha)
        if (Keyboard.current.leftArrowKey.isPressed)  
                direction.x = -1;
        if (Keyboard.current.rightArrowKey.isPressed) 
                direction.x = 1;
       
        if (direction.x!=0)
            lastDirection= direction;
            
        if(direction.x==0 && !Keyboard.current.shiftKey.isPressed)
            currSpeed=currSpeed*drag;
        
        //currSpeed=Mathf.Clamp(value:currSpeed,min:-maxSpeed,maxSpeed);
        transform.Translate(lastDirection.normalized * currSpeed *Time.deltaTime);
        if (direction.x==0 && currSpeed<0.1f) //Hay que guardar la direccion que llevaba, porque cuando dejan de pulsar se vuelve 0 y no se moveria
            lastDirection= Vector3.zero;


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
            o hacer que tenga alguna inercia (HECHO)
        */

        // Control de la aceleración al pulsar la tecla Shift
        var leftPressed=Keyboard.current.leftArrowKey.isPressed;
        var rightPressed=Keyboard.current.rightArrowKey.isPressed;
        if ((leftPressed || rightPressed) && Keyboard.current.shiftKey.isPressed)
        {
            if (currSpeed < maxSpeed)
                currSpeed += acceleration * Time.deltaTime;
        }
        if ((leftPressed || rightPressed) && !Keyboard.current.shiftKey.isPressed) 
            currSpeed = speed;
        

    }
}
