using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : Block
{
    public GameObject player;
    public GameObject coinPrefab;
    public GameObject coinParticles;
    public GameObject redBlockPrefab;
    public GameObject modelCoinBlock;


    void Start()
    {
        player=GameObject.FindWithTag("Player");
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
        GameObject redBlock=Instantiate(redBlockPrefab, transform.position,Quaternion.identity);
        modelCoinBlock.transform.gameObject.SetActive(false);
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

            //float tCoin=coinCurve.Evaluate(elapsedTime/animationTime);
            if(Mathf.RoundToInt(coin.transform.position.y*100f)<Mathf.RoundToInt(coinDesiredPosition.y*100f) && coinGoingDown==false)
            {
                Debug.Log("subiendo?");
                coin.transform.position=Vector3.LerpUnclamped(coin.transform.position, coinDesiredPosition,elapsedTime/animationTime);
            }

            else if(Mathf.RoundToInt(coin.transform.position.y*100f)>Mathf.RoundToInt(coinOriginalPosition.y*100f))
            {
                Debug.Log("bajando?");

                coinGoingDown=true;
                coin.transform.position=Vector3.LerpUnclamped(coin.transform.position, coinOriginalPosition,elapsedTime/animationTime);
            }
            else
            {
                Debug.Log("se acabó");
                player.GetComponent<Player>().coins++;
                Instantiate(coinParticles, coin.transform.position, Quaternion.identity);
                Destroy(coin);
                Destroy(transform.gameObject);

                yield break;
            }

            elapsedTime+=Time.deltaTime;
            yield return 0;
        }        
        yield return 0;
    }


}
