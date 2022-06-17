using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // public ParticleSystem particles;
    public GameObject particles;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 120.8f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            StartCoroutine( CoinDestroy() );
    }

    IEnumerator CoinDestroy()
    {
        yield return new WaitForEndOfFrame();
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
