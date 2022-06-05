using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject playerGameObject;
    public float timeToRespawn; 

    private Player player;

    void Start()
    {
        player = playerGameObject.GetComponent<Player>();
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
}
