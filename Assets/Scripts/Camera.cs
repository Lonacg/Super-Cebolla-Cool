using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public Rigidbody rigidbody;

    public float cameraHeight=4.5f;
    public float cameraWidth=-17f;
    public int playerDirection;

    void FixedUpdate()
    {
        FindDirection();

    }




    public void FindDirection()
    {
            
    }


}