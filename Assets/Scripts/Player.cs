using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Vector3 direction = PlayerControl();
        
        transform.Translate(
            direction * speed * Time.deltaTime
        );
    }

    private Vector3 PlayerControl()
    {
        Vector3 axis = Vector3.zero;   

        // Control del movimiento del jugador en el eje X (izquierda a derecha)
        if (Keyboard.current.leftArrowKey.isPressed)  
            axis.x = -1;
        if (Keyboard.current.rightArrowKey.isPressed) 
            axis.x =  1;

        // Control del salto del jugador en el eje Y
        if (Keyboard.current.upArrowKey.isPressed) 
            axis.y = 1;

        /*
            Dos propuestas para el control del salto:
                - Usar rigidbody junto con AddFoce() para impulsar un salto, 
                  en ese caso el código iría dentro de FixedUpdate() y debemos
                  especificar una gravedad (para mí la más sencilla y divertida por el tema de controlar la gravedad).

                - Usar una interpolación.
        */
            
        return axis;
    }
}
