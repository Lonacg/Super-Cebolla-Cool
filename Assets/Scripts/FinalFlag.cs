using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFlag : MonoBehaviour
{

    
    public delegate void PlayerWin(bool playerHasWin); 
    public static event PlayerWin OnPlayerWin;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        { 
            if (OnPlayerWin!=null)
                OnPlayerWin(this);
        }
    }
}
