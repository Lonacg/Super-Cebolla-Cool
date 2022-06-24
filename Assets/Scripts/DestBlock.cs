using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestBlock : Block
{
    public GameObject blockParticles;
    public delegate void BlockDestroyed(); 
    public static event BlockDestroyed OnBlockDestroyed;
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
        if (OnBlockDestroyed!=null)
            OnBlockDestroyed();
        Destroy(transform.gameObject);
        yield return 0;
    }


}