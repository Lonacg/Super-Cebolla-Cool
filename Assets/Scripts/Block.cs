using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockTypes {
        Destructible,
        Indestructible,
        Surprise,
        Struct
    };
    public BlockTypes blockType;
    private GameObject player;
    
    public GameObject coinPrefab;
    public GameObject niordoPrefab;
    public Material structMaterial;
    public GameObject brickPrefab;
    public GameObject coinParticles;
    public GameObject waterPrefab;
    private bool destroy;

    void Update()
    {
        if (destroy)
            Destroy(gameObject);
    }

    public void ChangeBlockState(GameObject player)
    {
        this.player = player;

        switch (blockType)
        {
            case BlockTypes.Destructible:
                OnDestructibleBlock();
                break;
            case BlockTypes.Indestructible:
                OnIndestructibleBlock();
                break;
            case BlockTypes.Surprise:
                OnSurpriseBlock();
                break;
            case BlockTypes.Struct:
                OnStructBlock();
                break;
        }
    }

    void OnDestructibleBlock()
    {
        Player p = player.GetComponent<Player>();
        if (p.playerState == Player.State.normal)
            return;
            
        StartCoroutine( OnDestroying() );
    }

    void OnSurpriseBlock()
    {
        StartCoroutine( OnBouncing() );

        switch (Random.Range(0, 3))
        {
            case 0:
                StartCoroutine(CoinLauncher());
                break;
            case 1:
                GameObject water = Instantiate(waterPrefab, 
                new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), 
                Quaternion.identity);
                break;
            case 2:
                StartCoroutine(NiordoLauncher());
                break;
        }
    }

    void OnIndestructibleBlock()
    {
        Debug.Log("Choque contra un bloque indestructible");
    }

    void OnStructBlock()
    {
        Debug.Log("Choque contra un bloque indestructible golpeado");
    }

    IEnumerator NiordoLauncher()
    {
        GameObject niordo = Instantiate(niordoPrefab, transform.position, Quaternion.identity);

        float mass = 1;
        float gravity = -1.2f;
        float force = 14;
        float speedY = 0;
        float gAccel = gravity / mass;
        float acceleration;

        while (speedY >= -0.1f)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            niordo.transform.Translate(Vector3.up * speedY);
            force = 0;

            yield return null;
        }

        while (true)
        {
            niordo.transform.Rotate(Vector3.up * Time.deltaTime * 62.8f);
            yield return null;
        }
    }

    IEnumerator CoinLauncher()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        float mass = 1;
        float gravity = -1.8f;
        float force = 16;
        float speedY = 0;
        float gAccel = gravity / mass;
        float acceleration;

        while (speedY >= -0.1f)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            coin.transform.Translate(Vector3.up * speedY);
            force = 0;

            yield return null;
        }

        player.GetComponent<Player>().coins++;
        Instantiate(coinParticles, coin.transform.position, Quaternion.identity);
        Destroy(coin);
    }

    IEnumerator OnBouncing()
    {
        float mass = 1;
        float gravity = -1.8f;
        float force = 9;
        float speedY = 0;
        float gAccel = gravity / mass;
        float acceleration;
        Vector3 oldPos = transform.position;

        while (transform.position.y >= oldPos.y)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            transform.Translate(Vector3.up * speedY);
            force = 0;

            yield return null;
        }

        transform.position = oldPos;
        blockType = BlockTypes.Struct;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = structMaterial;
    }

    IEnumerator OnDestroying()
    {
        float mass = 1;
        float gravity = -2.8f;
        float force = 11;
        float speedY = 0;
        float gAccel = gravity / mass;
        float acceleration;

        while (speedY >= 0)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            transform.Translate(Vector3.up * speedY);
            force = 0;

            yield return null;
        }

        transform.localScale = Vector3.zero;
        StartCoroutine( OnPropulsion() );
        StartCoroutine( OnPropulsion(-1) );
    }

    IEnumerator OnPropulsion(float horizontal = 1)
    {
        float mass = 1;
        float gravity = -1.2f;
        float force = 21;
        float speedY = 0;
        float gAccel = gravity / mass;
        float acceleration;
        float speedX = 12 * horizontal;

        GameObject brick = Instantiate(brickPrefab, transform.position, Quaternion.identity);

        while (speedY >= -0.1f)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            brick.transform.Translate(new Vector3(
                speedX * Time.deltaTime,
                speedY
            ));
            brick.transform.localScale += Vector3.one * -0.05f;
            force = 0;

            yield return null;
        }

        Destroy(brick);
        destroy = true;
    }
}
