using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    void Start()
    {
        StartMovement();
        enemyWidth=0.5f;
        enemyHeight=1f;
    }

    void FixedUpdate()
    {
        direction=AvoidHoles(direction);
        FixedUpdateMovement();
    }

    public int AvoidHoles(int dir)
    {
        //Rayo al suelo por delante
        Vector3 forwardRayOrigin=transform.position-Vector3.right*(enemyWidth-0.1f);
        Vector3 forwardRayDirection=-Vector3.up;
        Debug.DrawLine(forwardRayOrigin,forwardRayOrigin+forwardRayDirection*(enemyHeight+0.1f), Color.blue);
        if (!Physics.Raycast(forwardRayOrigin, forwardRayDirection, out RaycastHit forwardHitInfo,enemyHeight+0.1f)) //Si no te chocas con nada, cambia de direccion
            dir= dir*-1; 
        
        //Rayo al suelo por detras
        Vector3 backwardRayOrigin=transform.position+Vector3.right*(enemyWidth-0.1f);
        Vector3 backwardRayDirection=-Vector3.up;
        Debug.DrawLine(backwardRayOrigin,backwardRayOrigin+backwardRayDirection*(enemyHeight+0.1f), Color.blue);
        if (!Physics.Raycast(backwardRayOrigin, backwardRayDirection, out RaycastHit backwardHitInfo,enemyHeight+0.1f)) //Si no te chocas con nada, cambia de direccion
            dir= dir*-1;

        return dir;
    }
















}