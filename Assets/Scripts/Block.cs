using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    protected bool bouncing=false;

    public void BackwardCollisiion() //Colisiones por abajo
    {        
        Vector3 rayOrigin=transform.position+new Vector3(-0.5f,-0.5f,0);
        Vector3 rayDirection=Vector3.right;
        Debug.DrawLine(rayOrigin,rayOrigin+rayDirection, Color.blue);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo,1)) 
        {
            if (hitInfo.transform.tag =="Player" && bouncing==false)
                StartCoroutine(Bounce());  
        }

    }
    IEnumerator Bounce()
    {
        bouncing=true;
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
                    Debug.Log(transform.position);
                }
            

            else
            {
                bouncing=false;
                DoYourLastJob();
                yield break;
            }
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        bouncing=false;
        DoYourLastJob();       
        yield return 0;
    }


    public virtual void DoYourFirstJob()
    {
        
    }


    public virtual void DoYourLastJob()
    {
        
    }









}
