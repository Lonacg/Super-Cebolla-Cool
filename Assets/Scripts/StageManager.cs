using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject fishermanGo;
    public float timeToRespawn; 

    private Player player;

    void Start()
    {
        player = playerGameObject.GetComponent<Player>();
        StartCoroutine( FishermanRespawn() );
    }

    IEnumerator FishermanRespawn()
    {
        yield return new WaitForSeconds(5.2f);
        Instantiate(fishermanGo);
    }
}
