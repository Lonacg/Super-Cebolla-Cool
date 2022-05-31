using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        StartCoroutine( CoinDestroy() );
    }

    IEnumerator CoinDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
