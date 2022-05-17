using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private int counter;

    void Start()
    {
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Numero rebotes off
        if (counter < 0)
        {
            counter++;
            return;
        }

        Destroy(gameObject);
    }
}
