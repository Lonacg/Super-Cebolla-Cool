using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFlag : MonoBehaviour
{

    public delegate bool PlayerWinning(); 
    public static event PlayerWinning OnPlayerWinning;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        { 
            if (OnPlayerWinning!=null)
                OnPlayerWinning();
        }
    }
}
