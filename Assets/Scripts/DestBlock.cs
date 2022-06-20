using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestBlock : Block
{
    public GameObject blockParticles;

    void Start()
    {
        player=GameObject.FindWithTag("Player");
    }
    void Update()
    {

        BackwardCollisiion();        

    }

    public override void DestroyBlock()
    {
        StartCoroutine(DestroingBlock());
    }
    IEnumerator DestroingBlock()
    {
        Instantiate(blockParticles, transform.position,Quaternion.Euler(-90,0,0));
        
        yield return new WaitForSeconds(0.15f);
        Destroy(transform.gameObject);
        yield return 0;
    }


}