using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform enemyModel;
    private float speed;
    private int direction;
    private Vector3 desiredForward;
    private float enemyWidth; //radio del ancho del modelo de enemigo, para ajustar la longitud del raycast
    private float enemyHeight; //radio de la altura del modelo de enemigo, para ajustar la longitud del raycast
    //public bool beingTurtle;

    void Start()
    {
        enemyModel = transform.GetChild(0);
        direction=-1;
        if(gameObject.tag=="Turtle") //Datos caracteristicos de la tortuga
        {
            //beingTurtle=true;
            speed=4;
            enemyWidth=0.5f;
            enemyHeight=0.8f;
        }
        if(gameObject.tag=="Carapace") //Datos caracteristicos de la tortuga en estado caparazon
        {
            
            //enemyModel.transform.Rotate(new Vector3(90,90,180));
            //beingTurtle=true;
            speed=0;
            enemyWidth=0.45f;
            enemyHeight=0.2525f;
        }
        if(gameObject.tag=="Mushroom") //Datos caracteristicos de la seta
        {
            //beingTurtle=false;
            speed=3;
            enemyWidth=0.5f;
            enemyHeight=0.5f;
        }

    }

    void FixedUpdate()
    {
        if(gameObject.tag=="Turtle" || gameObject.tag=="Mushroom")
        {
            direction=CheckForwardsCollisionsAndChangeDirection(direction);
            direction=CheckBackwardCollisionWithPlayer(direction);
            CheckUpwardsCollisionWithPlayer();
            UpdateBodyRotation(direction);
            transform.Translate(Vector3.right*direction*speed*Time.deltaTime);
        }

    }

    private void UpdateBodyRotation(int dir)
    {
        desiredForward= new Vector3(0,0,-dir);
        enemyModel.forward=Vector3.Slerp(enemyModel.forward, desiredForward.normalized,Time.deltaTime*20);
    }

    public int CheckForwardsCollisionsAndChangeDirection(int dir) // colisiones por delante
    {
        Vector3 rayOrigin=transform.position-Vector3.up*0.3f;
        Vector3 rayDirection=Vector3.right*dir;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyWidth, Color.blue);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyWidth)) //Colision por delante
        {
            if (hitInfo.transform.tag!="Player")
            {
                dir= dir*-1;            
            }
            else Debug.Log("Ataque hecho a player");
        }
        return dir;
    }

    public int CheckBackwardCollisionWithPlayer(int dir) //Colisiones por detras
    {
        Vector3 rayOrigin=transform.position-Vector3.up*0.3f;
        Vector3 rayDirection=-Vector3.right*dir;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyWidth, Color.magenta);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyWidth)) //Colision por delante
        {
            if (hitInfo.transform.tag =="Player")
            {
                Debug.Log("Ataque hecho a player");
                dir= dir*-1;
            }
        }
        return dir;
    }


    public void CheckUpwardsCollisionWithPlayer() //Colisiones por arriba
    {
        Vector3 rayOrigin=transform.position;
        Vector3 rayDirection=Vector3.up;       
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyHeight, Color.yellow);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyHeight )) //Colision por delante
        {
            if (hitInfo.transform.tag =="Player")
            {
                Debug.Log("Enemigo muerto!");
                //LANZAR AQUI EL CODIGO EN EL QUE EL ENEMIGO SE MUERE SI ES CHAMPI O TORTUGA1
            }
        }
        
    }

}


