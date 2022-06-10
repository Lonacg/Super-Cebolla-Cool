using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject onion;
    
    protected Transform enemyModel;
    public float speed=1.5f;
    protected int direction;
    protected int lastDirection;
    protected Vector3 rayOffsetOriginSides;
    protected Vector3 rayOffsetOriginUpwards;
    protected Vector3 rayOffsetDirUpw;
    protected float enemyWidth; //radio del ancho del modelo de enemigo, para ajustar la longitud del raycast
    protected float enemyHeight; //radio de la altura del modelo de enemigo, para ajustar la longitud del raycast
    protected float enemyHead;
    protected Vector3 originalPosition;
    protected bool enemyDamaged;

    public virtual void StartEnemy()
    {
        onion=GameObject.Find("Onion");
        enemyModel = transform.GetChild(0);
        originalPosition=transform.position;
        direction=-1;
        lastDirection=-1;
        enemyDamaged=false;
    }

    public (int, bool) FixedUpdateMovement(int direction, bool enemyDamaged, Vector3 originalPosition, Vector3 rayOffsetDirUpw)
    {
        direction=CheckCollisions(direction, true); //colisiones por detras
        direction=CheckCollisions(direction, false); //coliiones por delante
        UpdateBodyRotation(direction);
 
        enemyDamaged=CheckUpwardsCollisionWithPlayer(enemyDamaged,rayOffsetDirUpw);
        direction= ActiveAndRecolocateEnemy(direction, enemyDamaged, originalPosition);
        return (direction,enemyDamaged);
    }

    public int CheckCollisions(int dir, bool backward=false) //=false es que si no le damos el valor de entrada lo one como false
    {
        Vector3 rayOrigin= transform.position+rayOffsetOriginSides;
        Vector3 rayDirection= Vector3.zero;
        if(!backward)         
            rayDirection=Vector3.right*dir;
        else
            rayDirection=-Vector3.right*dir;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyWidth, Color.magenta);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyWidth)) 
        {
            if (hitInfo.transform.tag!="Player")
            {
                dir= dir*-1;      
            }
            else
            {
                if(backward)
                {
                    dir= dir*-1; //cambio de direccion si player le toca por detras
                }
                Debug.Log("Ataque hecho a player");                
            }
        }
        return dir;
    }

    public void UpdateBodyRotation(int dir)
    {
        if(lastDirection!=dir)
        {
            StartCoroutine(TurnSlowly(dir));
        }
        lastDirection=dir;
        
    }
    IEnumerator TurnSlowly(int dir)
    {
        float elapsedTime=0;
        float TimeToTurn=0.8f;
        Vector3 desiredForward= new Vector3(0,0,-dir);
        while(elapsedTime<TimeToTurn)
        {
            enemyModel.forward=Vector3.Slerp(enemyModel.forward, desiredForward,elapsedTime/1.25f*TimeToTurn);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
    }

    public bool CheckUpwardsCollisionWithPlayer(bool eDamaged, Vector3 rayOffsetDirUpw) //Colisiones por arriba 
    {        
        Vector3 rayOrigin=transform.position+rayOffsetOriginUpwards;
        Vector3 rayDirection=Vector3.zero+rayOffsetDirUpw;
         
        
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection*enemyHead, Color.yellow);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,enemyHead)) 
        {
            if (hitInfo.transform.tag =="Player")
            {
                eDamaged=true;
            }
        }
        else 
            eDamaged=false;        
        return eDamaged;
    }

    public int ActiveAndRecolocateEnemy(int dir, bool eDamaged, Vector3 oPosition)
    {
        float currentDistanceWithPlayer= Vector3.Magnitude(transform.position-onion.transform.position);
        float distanceFromOriginToPlayer= Vector3.Magnitude(originalPosition-onion.transform.position);        
        if(currentDistanceWithPlayer<=20 || distanceFromOriginToPlayer<=20) 
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