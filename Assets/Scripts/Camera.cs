using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{


    public float cameraHeight=4.5f;
    public float cameraWidth=-17f;
    public int playerDirection=0;
    public int lastPlayerDirection;
    public float playerPosition;
    public int playerRotation;
    public float lastPlayerPosition;
    public Vector3 cameraPosition;
    private Rigidbody rb;
    private GameObject onion;
    private GameObject onionModel;
    private float offset;
    public bool directionChanged;
    public float timeSaved;

    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFramingTransposer framingTransposer;



    void Start()
    {
        onion=GameObject.Find("Onion");
        rb=onion.GetComponent<Rigidbody>();
        onionModel=GameObject.Find("OnionModel");
    
        framingTransposer=virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        offset=framingTransposer.m_TrackedObjectOffset.x;

        cameraPosition.x= framingTransposer.m_TrackedObjectOffset.x;
        cameraPosition.y= cameraHeight;
        cameraPosition.z= cameraWidth;


    }

    void FixedUpdate()
    {
        playerRotation= GetPlayerRotation();   
        (playerDirection,lastPlayerDirection)= GetPlayerDirection(playerDirection);
        (directionChanged,timeSaved)= CheckDirectionsChanges(playerDirection,lastPlayerDirection);

        //Debug.Log(directionChanged);
        //Debug.Log(timeSaved);


        ChangeCameraPosition(playerRotation, cameraPosition, directionChanged, timeSaved); 


    }



    public void ChangeCameraPosition(float plaRotation, Vector3 camPosition, bool dirChanged, float tiSaved) //se cambia el Tracked Object Offset de la camara virtual
    {

        //Debug.Log("rotacion: " + pRotation);
        if ((plaRotation!= 90 && plaRotation!= 270) || (rb.velocity.x<=0.05f && rb.velocity.x>=-0.05f))
            return;
        
                
        if (rb.velocity.x>0.05f)
        {
            //if(dirChanged)

            if(offset<3.99)
            {
                offset=(rb.velocity.x*(Time.time-tiSaved))*2;
                //Debug.Log(offset);
            }
            else
                offset=4;

        }
        else if (rb.velocity.x<-0.05f) //el if no haria falta, es eso directamente 
        {
            if(offset>-3.99)
            {
                offset=-(rb.velocity.x*(Time.time-tiSaved))*2;
                //Debug.Log(offset);
            }
            else
                offset=4;
        }



    }




    public (int, int) GetPlayerDirection(int pDirection)
    {
        lastPlayerDirection=pDirection;
        if (rb.velocity.x>0)
            pDirection=1;
        else if (rb.velocity.x<0)
            pDirection=-1;
        else 
            pDirection=0;    

        return (pDirection, lastPlayerDirection);
    }

    public (bool, float) CheckDirectionsChanges(int pDirection, int lastPDirection)
    {
        if(pDirection!=lastPDirection)
        {
            directionChanged=true;
            timeSaved=Time.time;
        }
        else
        {
            directionChanged=false;
            timeSaved=0;
        }

        return (directionChanged, timeSaved);
    }



    public float GetPlayerPosition()
    {
        playerPosition=rb.position.x;
        return playerPosition;
    }
    public int GetPlayerRotation()
    {
        playerRotation=Mathf.RoundToInt(onionModel.transform.eulerAngles.y);
        return playerRotation;
    }


}