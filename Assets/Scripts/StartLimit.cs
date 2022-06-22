using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLimit : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag!="Player")
        {
            Physics.IgnoreCollision(other.transform.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }

    }
    

}
