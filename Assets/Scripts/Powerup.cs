using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameObject player;
    private float dropWaterVelocity=3;
    private bool beingWaterDrop=false;
    private int direction=1;

    void Start()
    {
        player=GameObject.FindWithTag("Player");

        if (transform.gameObject.tag=="WaterDrop")
        {
            beingWaterDrop=true;
        }

    }

    void FixedUpdate()
    {
        float currentDistanceWithPlayer= Vector3.Magnitude(transform.position-player.transform.position);
        if(currentDistanceWithPlayer>26)
            Destroy(transform.gameObject);
        if (beingWaterDrop)
        {
            //direction= WaterDropMovement(direction);
            transform.Translate(Vector3.right*direction*dropWaterVelocity*Time.deltaTime);
        }


    }

    int OnCollisionEnter(Collision collision)
    {
        //if(collision.transform.tag!="Untagged")
        Debug.Log("POWERUP " + collision.transform.tag);
        Debug.Log("EL OTRO "+ collision.transform.position.y);
        Debug.Log("EL "+ transform.position.y);
        if (Mathf.RoundToInt(collision.transform.position.y*100)>=Mathf.RoundToInt(transform.position.y*100) && collision.transform.tag!="StartLimit"&& collision.transform.tag!="Player"&& collision.transform.tag!="DestructibleBlock"&& collision.transform.tag!="Block")
            direction=direction*(-1);

        return direction;
    }
    /*public int WaterDropMovement(int dir)
    {

        Vector3 rayOrigin=transform.position-Vector3.right*0.50f;
        float diameter=1; 
        Debug.DrawLine(rayOrigin,rayOrigin+Vector3.right*(diameter), Color.magenta);
        if (Physics.Raycast(rayOrigin, Vector3.right, out RaycastHit hitInfo,diameter)) 
        {
            if (hitInfo.transform.tag!="Player" && hitInfo.transform.tag!="StartLimit")
            {
                dir= dir*-1;      
            }
        }
        return dir;
    }*/


}
