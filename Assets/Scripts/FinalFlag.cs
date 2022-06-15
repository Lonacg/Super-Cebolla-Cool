using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFlag : MonoBehaviour
{

    public GameObject player;
    public GameObject playerModel;
    public GameObject confetiParticlesPrefab;

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
            //LANZAR DESDE AQUI EL SONIDO DE VICTORIA
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
        Vector3 desiredPosition = new Vector3(236.75f,1+heightPlayer,0);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.y*100f)>Mathf.Round(desiredPosition.y*100f))
        {
            player.transform.position=Vector3.Lerp(player.transform.position, desiredPosition,elapsedTime/20);
            elapsedTime+=Time.deltaTime;
            yield return 0; 
        }

        StartCoroutine(Turning());
        yield return 0;
    }

    IEnumerator Turning()
    {
        Vector3 desiredPosition = new Vector3(238.25f,1+heightPlayer,0);
        Quaternion desiredRotation= Quaternion.Euler(0,180,0);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.x*100f)<Mathf.Round(desiredPosition.x*100f))
        {
            player.transform.position=Vector3.Lerp(player.transform.position, desiredPosition,elapsedTime/20);
            player.transform.rotation=Quaternion.Lerp(player.transform.rotation, desiredRotation,elapsedTime/20);
            elapsedTime+=Time.deltaTime;
            yield return 0; 
        }
        player.transform.rotation=desiredRotation;
        StartCoroutine(Jumping());
        yield return 0;
    }

    IEnumerator Jumping()
    {
        Rigidbody rigidbody= player.GetComponent<Rigidbody>();
        rigidbody.useGravity=true;
        rigidbody.AddForce(new Vector3(2,3.5f,0)*2, ForceMode.Impulse);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.y*100f)>heightPlayer*100f)
        {
            if(player.transform.position.y<3.11f+heightPlayer&& rigidbody.velocity.y>0)
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(180,0,0),elapsedTime/3);
            else if(player.transform.position.y<3.41f+heightPlayer&& rigidbody.velocity.y>0)
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,0,-180),elapsedTime/3);
            else
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,180,0),elapsedTime/3);

            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        player.GetComponent<Rigidbody>().velocity=Vector3.zero;

        StartCoroutine(Fireworks());
        yield return 0;
    }


    /*IEnumerator Jumping()
    {
        Vector3 desiredPosition0 = new Vector3(238.9f,3.42f,0);
        Vector3 desiredPosition1 = new Vector3(239.7f,4.22f,0);
        Vector3 desiredPosition2 = new Vector3(240.7f,4.4f,0);
        Vector3 desiredPosition3 = new Vector3(241.8f,4.78f,0);
        Vector3 desiredPosition4 = new Vector3(243.7f,4.22f,0);
        Vector3 desiredPosition5 = new Vector3(245.7f,3.49f,0);
        Vector3 desiredPosition6 = new Vector3(246.5f,heightPlayer,0);
        List<Vector3> positions = new List<Vector3>() {desiredPosition0, desiredPosition1,desiredPosition2,desiredPosition3,desiredPosition4,desiredPosition5,desiredPosition6};
        Debug.Log(positions.Count);
        Quaternion desiredRotationModel= Quaternion.Euler(180,0,0);
        float elapsedTime=0;

        for (var i=0; i< positions.Count; i++)
        {            
            while(Mathf.Round(player.transform.position.x*100f)<Mathf.Round(positions[i].x*100f))
            {        
                if (i==0)
                    playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(180,0,0),elapsedTime/3); 
                
                else if(i==1)
                    playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,0,-180),elapsedTime/3); 

                else if (i==2)
                    playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,-180,0),elapsedTime/3);

                player.transform.position=Vector3.Lerp(player.transform.position, positions[i],elapsedTime/3);
                
                //playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, desiredRotationModel,elapsedTime/3);
                elapsedTime+=Time.deltaTime;
                yield return 0;
            }
            yield return 0;
        }
        StartCoroutine(Fireworks());
        yield return 0;
    }*/

    IEnumerator Fireworks()
    {
        yield return new WaitForSeconds(0.5f); 
        GameObject fireworks=Instantiate(confetiParticlesPrefab, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2); 
        StartCoroutine(GoingOut());
        yield return 0;       
    }

    IEnumerator GoingOut()
    {
        playerModel.transform.rotation=Quaternion.Euler(0,90,0);
        float elapsedTime=0;
        Vector3 desiredPosition=new Vector3(262f,heightPlayer,0);
        while(player.transform.position.x<262f)
        {
            player.transform.position=Vector3.Lerp(player.transform.position,desiredPosition,elapsedTime/50);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        //LLAMAR AQUI A LA UI PARA EL CAMBIO DE PANTALLA
        Debug.Log("Fin");
        yield return 0;
    }
}
