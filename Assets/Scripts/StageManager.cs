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

    void Update()
    {
        if (player.playerState == Player.State.dead)
            StartCoroutine( PlayerRespawn() );
    }

    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        player.playerState = Player.State.normal;
        playerGameObject.transform.position = new Vector3(-5.5f, 4, 0);
    }

    IEnumerator FishermanRespawn()
    {
        yield return new WaitForSeconds(5.2f);
        Instantiate(fishermanGo);
    }
}
