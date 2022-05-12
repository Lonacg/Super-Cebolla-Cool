using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public new Rigidbody rigidbody;

    public float cameraHeight=4.5f;
    public float cameraWidth=-17f;
    public int playerDirection;
    public float playerPosition;
    public float lastPlayerPosition;
    public float cameraPosition=0;
    public float lastCameraPosition=0;

    private Vector3 oldPosition;
    private Vector3 newPosition;

    void FixedUpdate()
    {
        playerDirection= GetPlayerDirection();
        playerPosition= GetPlayerPosition();

        CameraMovement();

        lastPlayerPosition=playerPosition;
        lastCameraPosition=cameraPosition;
    }



    public void CameraMovement()
    {
        cameraPosition= playerPosition;

        oldPosition=new Vector3(lastCameraPosition,cameraHeight, cameraWidth);
        newPosition=new Vector3(cameraPosition,cameraHeight, cameraWidth);

        transform.position=Vector3.Lerp(oldPosition,newPosition, Time.fixedDeltaTime);


    }


    public int GetPlayerDirection()
    {
        if (rigidbody.velocity.x>0)
            playerDirection=1;
        else if (rigidbody.velocity.x<0)
            playerDirection=-1;
        else 
            playerDirection=0;    

        return playerDirection;
    }

    public float GetPlayerPosition()
    {
        playerPosition=rigidbody.worldCenterOfMass.x;
        return playerPosition;
    }



}