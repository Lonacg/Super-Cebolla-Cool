using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBlock : Block
{
    public GameObject modelBlock;
    public GameObject redBlockPrefab;
    public GameObject waterDropPrefab;
    public GameObject  poopPrefab;
    private GameObject parentBlocks; //el padre donde queremos que instancie el bloque
    private Vector3 powerupOriginalPosition;
    
    void Start()
    {
        player=GameObject.FindWithTag("Player");
        parentBlocks=GameObject.FindWithTag("ParentBlocks");
        powerupOriginalPosition=transform.position;

    }

    void Update()
    {
        BackwardCollisiion();
    }

    public override void DoYourFirstJob()
    {
        
    }


    public override void DoYourLastJob()
    {
        transform.gameObject.GetComponent<BoxCollider>().enabled=false;
        StartCoroutine(GivePowerup());
        modelBlock.transform.gameObject.SetActive(false);  //desactivo el modelo del sorpresa para que no se vea, no se puede borrar hasta que no se acabe la rutina de salida del power up
        GameObject redBlock=Instantiate(redBlockPrefab, transform.position,Quaternion.identity, parentBlocks.transform); //instancio el rojo
    }

    IEnumerator GivePowerup()
    {
        //INICIAR AQUI SONIDO DE SALIDA DE POWERUP
        GameObject powerup;
        if(player.transform.localScale.y==0.5f)
        {
            powerup=Instantiate(waterDropPrefab, transform.position,Quaternion.identity);
        }
            
        else
        {
            powerup=Instantiate(poopPrefab, transform.position,Quaternion.identity);
        }
        powerup.transform.gameObject.GetComponent<Powerup>().enabled=false; //desactivo el script mientras sale el powerup
            
        Vector3 powerupDesiredPosition=powerupOriginalPosition+Vector3.up;

        float elapsedTime=0;
        while(Mathf.RoundToInt(powerup.transform.position.y*100f)<Mathf.RoundToInt(powerupDesiredPosition.y*100f))
        {
            powerup.transform.position=Vector3.LerpUnclamped(powerup.transform.position, powerupDesiredPosition,elapsedTime/10);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        powerup.transform.gameObject.GetComponent<Powerup>().enabled=true;   
        bouncing=false; //esta creo que no hace falta, pero paporsi
        Destroy(transform.gameObject);

     
        yield return 0;


    }




}
