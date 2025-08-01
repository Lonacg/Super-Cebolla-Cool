using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : Block
{
    public GameObject coinPrefab;
    public GameObject coinParticles;
    public GameObject redBlockPrefab;
    public GameObject modelBlock;
    private GameObject parentBlocks; //el padre donde queremos que instancie el bloque

    public delegate void CoinDestroying(); 
    public static event CoinDestroying OnCoinDestroying;

    void Start()
    {
        player=GameObject.FindWithTag("Player");
        parentBlocks=GameObject.FindWithTag("ParentBlocks");
        
    }

    void Update()
    {
        BackwardCollisiion();
    }


    public override void DoYourFirstJob()
    {
        StartCoroutine(GiveCoin());
    }


    public override void DoYourLastJob()
    {
        transform.gameObject.GetComponent<BoxCollider>().enabled=false; //Desactivo el colider del bloque sorpresa para que no choque con el rojo
        modelBlock.transform.gameObject.SetActive(false);  //desactivo el modelo del sorpresa para que no se vea, , no se puede borrar hasta que no se acabe la rutina de GiveCoin
        GameObject redBlock=Instantiate(redBlockPrefab, transform.position,Quaternion.identity, parentBlocks.transform); //instancio el rojo
        
    }

    IEnumerator GiveCoin()
    {
        GameObject coin=Instantiate(coinPrefab, transform.position+Vector3.up,Quaternion.identity);
        Vector3 coinOriginalPosition=coin.transform.position;
        Vector3 coinDesiredPosition=coinOriginalPosition+Vector3.up;

        float elapsedTime=0;
        float animationTime=2;
        bool coinGoingDown=false;
        while(elapsedTime<animationTime)
        {
            if(coin==null) break;
            //float tCoin=coinCurve.Evaluate(elapsedTime/animationTime);
            if(Mathf.RoundToInt(coin.transform.position.y*100f)<Mathf.RoundToInt(coinDesiredPosition.y*100f) && coinGoingDown==false)
            {
                coin.transform.position=Vector3.LerpUnclamped(coin.transform.position, coinDesiredPosition,elapsedTime/animationTime);
            }

            else if(Mathf.RoundToInt(coin.transform.position.y*100f)>Mathf.RoundToInt(coinOriginalPosition.y*100f))
            {
                coinGoingDown=true;
                coin.transform.position=Vector3.LerpUnclamped(coin.transform.position, coinOriginalPosition,elapsedTime/animationTime);
            }
            else
            {
                player.GetComponent<Player>().coins++;
                Instantiate(coinParticles, coin.transform.position, Quaternion.identity);
                Destroy(coin);
                if (OnCoinDestroying!=null)
                    OnCoinDestroying();
                Destroy(transform.gameObject);
                bouncing=false;
                yield break;
            }

            elapsedTime+=Time.deltaTime;
            yield return 0;
        }        
        yield return 0;
    }


}
