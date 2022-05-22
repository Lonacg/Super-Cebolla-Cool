using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform enemyModel;
    private float speed;
    private int direction;
    private Vector3 desiredForward;
    private float enemyWidth; //radio del modelo de enemigo, para ajustar la longitud del raycast

    void Start()
    {
        enemyModel = transform.GetChild(0);
        direction=-1;
        if(gameObject.tag=="Tortuga") //Datos caracteristicos de la tortuga
        {
            speed=4;
            enemyWidth=0.5f;
        }
        if(gameObject.tag=="Seta") //Datos caracteristicos de la seta
        {
            speed=3;
            enemyWidth=0.5f;
        }
    }

    void FixedUpdate()
    {
        direction=CheckCollisionsAndChangeDirection(direction);
        UpdateBodyRotation(direction);
        transform.Translate(Vector3.right*direction*speed*Time.deltaTime);
    }

    private void UpdateBodyRotation(int dir)
    {
        desiredForward= new Vector3(0,0,-dir);
        enemyModel.forward=Vector3.Slerp(enemyModel.forward, desiredForward.normalized,Time.deltaTime*20);
    }

    public int CheckCollisionsAndChangeDirection(int dir)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position-Vector3.up*0.1f,transform.position+(Vector3.right*dir)*enemyWidth);
        Debug.DrawLine(transform.position-Vector3.up*0.1f,transform.position+(Vector3.right*dir)*enemyWidth, Color.blue);
        if (Physics.Raycast(transform.position-Vector3.up*0.1f, Vector3.right*direction, out hitInfo,enemyWidth))
        {
            if (hitInfo.transform.name!="Onion")
            {
                dir= dir*-1;
            }
        }
        return dir;
    }
}
