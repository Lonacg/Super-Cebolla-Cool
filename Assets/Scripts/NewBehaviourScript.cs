using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float variable=3;
    void Update()
    {
        variable=Funcion1(variable);
        Debug.Log("update: " + variable);

    }

    public float Funcion1(float v)
    {
        v++;
        Debug.Log("funcion: " + v);
        return v;

    }
}
