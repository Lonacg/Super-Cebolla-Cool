using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    void Start()
    {
        StartMovement();
    }

    void FixedUpdate()
    {
        FixedUpdateMovement();
    }
}