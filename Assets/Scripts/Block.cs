using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected GameObject player;

    protected bool bouncing=false;
    protected bool destroying=false;

    public void BackwardCollisiion() //Colisiones por abajo
    {        
        Vector3 rayOrigin=transform.position+new Vector3(-0.5f,-0.5f,0);
        Vector3 rayDirection=Vector3.right;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection, Color.blue);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,1)) 
        {
            Debug.Log("choque con: " + hitInfo.transform.tag);
            Debug.Log("ejecutando bouncing "+ bouncing);
            if (hitInfo.transform.tag =="Player" && bouncing==false && destroying==false)
            {                
                if(player.transform.localScale.y==1 && transform.gameObject.tag=="DestructibleBlock") //este if No deberia hacerse asi porque es la clase base... pero es mas rapido 
                {
                    destroying=true;
                    DestroyBlock();
                }
                bouncing=true;
                StartCoroutine(Bounce());
                
            }

        }

    }
    IEnumerator Bounce()
    {
        float elapsedTime=0;
        float animationTime=0.5f;
        bool blockGoingDown=false;
        Vector3 originalPosition=transform.position;            
        Vector3 desiredPosition=originalPosition+Vector3.up*0.3f;
        while(elapsedTime<animationTime)
        {
            if(Mathf.RoundToInt(transform.position.y*100f)< Mathf.RoundToInt(desiredPosition.y*100f) && blockGoingDown==false)
            {
                transform.position=Vector3.Lerp(transform.position, desiredPosition,elapsedTime/animationTime);
            }
            else if (Mathf.RoundToInt(transform.position.y*100f)> Mathf.RoundToInt(originalPosition.y*100f))
                {                
                    if(!blockGoingDown)
                        DoYourFirstJob();
                    blockGoingDown=true;
                    transform.position=Vector3.Lerp(transform.position, originalPosition,elapsedTime/animationTime);
                }

            else
            {                
                DoYourLastJob();               
                yield break;
            }
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        DoYourLastJob();
        yield return 0;
    }


    public virtual void DoYourFirstJob()
    {
        
    }


    public virtual void DoYourLastJob()
    {
        bouncing=false;
    }


    public virtual void DestroyBlock()
    {

    }






}
