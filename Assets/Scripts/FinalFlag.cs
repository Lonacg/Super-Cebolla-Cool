using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFlag : MonoBehaviour
{

    public GameObject player;
    public bool hasWon=false;
    private bool finalAnimationActive=false;

    private float heightPlayer=0.5f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        { 
            player.GetComponent<Player>().enabled = false;
            player.GetComponent<Rigidbody>().velocity=Vector3.zero;
            player.GetComponent<Rigidbody>().useGravity=false;

            transform.gameObject.GetComponent<Collider>().enabled = false;
            heightPlayer=player.transform.localScale.y;
            hasWon=true;

        }

    }

    void Update()
    {
        if(hasWon==true&& finalAnimationActive==false)
        {
            finalAnimationActive=true;
            StartCoroutine(PlayerGoingDown());
        }

    }

    IEnumerator PlayerGoingDown()
    {
        //yield return new WaitForSeconds(0.5f);
        Vector3 desiredPosition = new Vector3(236.75f,1+heightPlayer,0);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.y*100f)>Mathf.Round(desiredPosition.y*100f))
        {
            player.transform.position=Vector3.Lerp(player.transform.position, desiredPosition,elapsedTime/30);
            elapsedTime+=Time.deltaTime;
            yield return 0; 
        }

        StartCoroutine(Turning());
        yield return 0;
    }

    IEnumerator Turning()
    {
        



        //StartCoroutine(Jumping());
        yield return 0;
    }

    IEnumerator Jumping()
    {

        //StartCoroutine(MovingTowardsCastle());
        yield return 0;
    }

    IEnumerator MovingTowardsCastle()
    {
        float elapsedTime=0;
        Vector3 desiredPosition=new Vector3(247.5f,heightPlayer,0);
        while(player.transform.position.x<247.5f)
        {
            player.transform.position=Vector3.Lerp(player.transform.position,desiredPosition,elapsedTime/50);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        Debug.Log("he llegado a la puerta");
        //AQUI EL GIRO DE FRENTE Y EL LANZAMIENTO DE CONFETIS
        yield return 0;
    }





}
