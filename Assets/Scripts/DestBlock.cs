using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestBlock : Block
{
    public GameObject player;
    public GameObject blockParticles;

    void Start()
    {
        player=GameObject.FindWithTag("Player");
    }
    void Update()
    {
        //NO SE PUEDE LANZAR ASI, TIENE QUE COMPROBARLO EN EL MOMENTO EN EL QUE CHOCA, NO CUANDO SE PONE GRANDE POR PRIMERA VEZ
        if (player.transform.localScale.y==0.5f) //Si player es pequeño, haz el rebote de bloque
        {
            BackwardCollisiion();        
        }
        else //si eres grande, la animacion de destruccion
            StartCoroutine(DestroyBlock());
    }

    IEnumerator DestroyBlock()
    {
        GameObject redBlock=Instantiate(blockParticles, transform.position,Quaternion.identity);

        yield return new WaitForSeconds(2);
    }




}