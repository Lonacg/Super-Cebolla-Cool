using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinParticles;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 250f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Instantiate(coinParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
