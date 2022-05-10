using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public float cameraHeight=4.5f;
    public float cameraWidth=-17f;

    private float playerPositionX;

    private float playerDirection;
    private float lastPlayerDirection;
    //private float lastPlayerPositionX;
    private float playerPositionXSaved;
    private bool directionChanged=false;

    private float moveAwayPosition=0; //posicion de la camara segun se mueve el personaje 

    void FixedUpdate()
    {

        CambioDireccion();
        playerPositionX= player.transform.position.x;
        //Debug.Log(lastPlayerPositionX);
        //Debug.Log(playerPositionX);
        //Debug.Log("playerDirection: "+playerDirection);
        //Debug.Log("playerPositionX: "+playerPositionX);

        //Debug.Log("lastPlayerDirection: "+lastPlayerDirection);
        //Debug.Log("lastPlayerPositionX: "+lastPlayerPositionX);


        //Debug.Log("moveAwayPosition: "+ moveAwayPosition);
        //Debug.Log("newTargetPositionX: "+ newTargetPositionX);
        //Debug.Log("lastTargetPositionX: "+ lastTargetPositionX);
        
        //Debug.Log("newTargetPositionX/2: "+ newTargetPositionX/2);
        if(playerDirection==-1)
        {

            if(lastPlayerDirection==1)
            {
                directionChanged=true;
                playerPositionXSaved=playerPositionX; //bien
            }
            //Debug.Log("DE 1 A -1: " + directionChanged);
            if (directionChanged)
            {
                //Debug.Log("hola?");
                Debug.Log(moveAwayPosition);
                if(moveAwayPosition>playerPositionX-0.5)
                {
                    Debug.Log("hola");
                    //Debug.Log("ANTERIOR moveAwayPosition: "+ moveAwayPosition);

                    moveAwayPosition=moveAwayPosition-(playerPositionXSaved-playerPositionX)/2;
                    //Debug.Log("NUEVO moveAwayPosition: "+ moveAwayPosition);

                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
                }
                else
                    directionChanged=false;
                
            }
            else
            {
                //Debug.Log(moveAwayPosition>playerPositionX-3.8);
                if(moveAwayPosition>playerPositionX-3.8)
                {
                    //Debug.Log("hola?");                    
                    moveAwayPosition=playerPositionX-(playerPositionX/2);
                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);

                }
                else
                {
                    //Debug.Log("hola?");
                    //Debug.Log(moveAwayPosition);
                    moveAwayPosition=playerPositionX-4;
                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
                }
            }
        }
        if (playerDirection==1)
        {
            if (lastPlayerDirection==-1)
            {
                directionChanged=true;
                playerPositionXSaved=playerPositionX;
            }
            //Debug.Log("DE -1 A 1: " + directionChanged);

            if(directionChanged)
            {
                if(moveAwayPosition<playerPositionX+0.5f)
                {
                    moveAwayPosition=moveAwayPosition+(playerPositionXSaved-playerPositionX)/2;
                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
                }
                else
                    directionChanged=false;
              
            }
            else
            {
                if(moveAwayPosition<playerPositionX+3.8f)
                {
                    moveAwayPosition=playerPositionX+(playerPositionX/2);
                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);

                }
                else
                {
                    //Debug.Log(moveAwayPosition);
                    moveAwayPosition=playerPositionX+4;
                    transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
                }
            }
        }



        /*
        else if(lastPlayerDirection==-1 && playerDirection==1)
        {
            moveAwayPosition=playerPositionX+4;
            transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);

        }
        if (playerDirection==1)
        {
            
            if(moveAwayPosition<playerPositionX+4)
            {
                moveAwayPosition=playerPositionX+(playerPositionX/2);
                transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
            }
            else
            {
                moveAwayPosition=playerPositionX+4;
                transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);
            }

        }

        else if (playerDirection==-1)
        {
            //Debug.Log(moveAwayPosition);
            if(moveAwayPosition>playerPositionX-4)
            {
                moveAwayPosition=playerPositionX+(playerPositionX/2);
                transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);

            }    
            else
            {
                moveAwayPosition=playerPositionX-4;
                transform.position=new Vector3(moveAwayPosition,cameraHeight, cameraWidth);

            }

        }*/
        
        
        
        //lastPlayerPositionX=player.transform.position.x;

    }


    void CambioDireccion()
    {
        lastPlayerDirection=playerDirection;
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            playerDirection=1;
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            playerDirection=-1;
        }
        else
            playerDirection=0;

    }




}