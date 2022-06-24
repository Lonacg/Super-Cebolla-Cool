using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinParticles;

    public delegate void CoinDestroying(); 
    public static event CoinDestroying OnCoinDestroying;
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 250f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {

            Instantiate(coinParticles, transform.position, Quaternion.identity);
            if (OnCoinDestroying!=null)
                OnCoinDestroying();
            Destroy(gameObject);
        }
    }


}
