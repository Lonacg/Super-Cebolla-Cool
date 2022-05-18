using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float lifetime;
    private int counter;
    private float elapsedTime;

    void Start()
    {
        
    }

    void Update()
    {
        if (lifetime == 0)
            return;

        if (elapsedTime < lifetime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
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
