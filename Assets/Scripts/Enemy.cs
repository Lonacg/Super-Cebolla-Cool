using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject onion;
    protected Transform enemyModel;
    public float speed=2;
    protected int direction;
    protected int lastDirection;
    protected Vector3 desiredForward;
    protected Vector3 rayOrigin;
    protected float enemyWidth; //radio del ancho del modelo de enemigo, para ajustar la longitud del raycast
    protected float enemyHeight; //radio de la altura del modelo de enemigo, para ajustar la longitud del raycast
    protected Vector3 originalPosition;


    public void StartMovement()
    {
        onion=GameObject.Find("Onion");
        enemyModel = transform.GetChild(0);
        originalPosition=transform.position;
        direction=-1;
        lastDirection=-1;
        if(gameObject.tag=="Turtle") //Datos caracteristicos de la tortuga
        {
            enemyWidth=0.5f;
            enemyHeight=1f;
        }
        if(gameObject.tag=="Carapace") //Datos caracteristicos de la tortuga en estado caparazon
        {
            //enemyModel.transform.Rotate(new Vector3(90,90,180));
            speed=0;
            enemyWidth=0.45f;
            enemyHeight=0.2525f;
        }
        if(gameObject.tag=="Mushroom") //Datos caracteristicos de la seta
        {
            enemyWidth=0.5f;
            enemyHeight=0.5f;
        }
    }

    public void FixedUpdateMovement()
    {
        direction=CheckForwardsCollisionsAndChangeDirection(direction);
        direction=CheckBackwardCollisionWithPlayer(direction);
        CheckUpwardsCollisionWithPlayer();
        UpdateBodyRotation(direction);
        direction= ActiveAndRecolocateEnemy(direction);
    }



    public int CheckForwardsCollisionsAndChangeDirection(int dir) // colisiones por delante
    {
        if(gameObject.tag=="Turtle")
        {
            rayOrigin=transform.position-Vector3.up*0.1f;
        }
        else
        {
            rayOrigin=transform.position+Vector3.up*0.4f;
        }

        Vector3 rayDirection=Vector3.right*dir;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyWidth, Color.blue);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyWidth)) 
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
        if(gameObject.tag=="Turtle")
        {
            rayOrigin=transform.position-Vector3.up*0.1f;
        }
        else
        {
            rayOrigin=transform.position+Vector3.up*0.4f;
        }
        Vector3 rayDirection=-Vector3.right*dir;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyWidth, Color.magenta);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyWidth)) 
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
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyHeight )) 
        {
            if (hitInfo.transform.tag =="Player")
            {
                Debug.Log("Enemigo muerto!");
                //LANZAR AQUI EL CODIGO EN EL QUE EL ENEMIGO SE MUERE SI ES CHAMPI O TORTUGA1
            }
        }
    }

    public void UpdateBodyRotation(int dir)
    {
        if(gameObject.tag=="Turtle" || gameObject.tag=="Mushroom")
        {
            if(lastDirection!=dir)
            {
                StartCoroutine(TurnSlowly(dir));
            }
            lastDirection=dir;
        }
    }
    IEnumerator TurnSlowly(int dir)
    {
        float elapsedTime=0;
        float TimeToTurn=0.8f;
        desiredForward= new Vector3(0,0,-dir);
        while(elapsedTime<TimeToTurn)
        {
            enemyModel.forward=Vector3.Slerp(enemyModel.forward, desiredForward,elapsedTime/1.25f*TimeToTurn);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
    }

    public int ActiveAndRecolocateEnemy(int dir)
    {
        float currentDistanceWithPlayer= Vector3.Magnitude(transform.position-onion.transform.position);
        float distanceFromOriginToPlayer= Vector3.Magnitude(originalPosition-onion.transform.position);        
        if(currentDistanceWithPlayer<=20 ||distanceFromOriginToPlayer<=20) 
        {
            transform.Translate(Vector3.right*dir*speed*Time.deltaTime); //si player esta cerca del enemigo, este se mueve
        }
        else 
        {
            transform.position=originalPosition; //si player se ha alejado, el enemigo vuelve a su posicion
            if (onion.transform.position.x>transform.position.x) //cuando vuelva a aparecer player, el enemigo se movera desde su psicion origen con direccion hacia player 
                dir=1;   
            else
                dir=-1;
        }
        return dir;
    }

}