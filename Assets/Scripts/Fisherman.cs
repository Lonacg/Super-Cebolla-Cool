using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : MonoBehaviour
{
    private GameObject player;
    private Transform oscilantMov;
    private bool onPlayer;
    private bool bombAvailable;
    private float fireRateCounter;

    public GameObject bombPrefab;
    public float bombFireRate;
    public float moveSpeed = 0.3f;
    public float xAmplitude = 5.8f;
    public float xFrequency = 2.1f;
    public float yAmplitude;
    public float yFrequency;
    public float range = 0.05f;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(-40.2f, 10.8f, 0);  
        oscilantMov = transform.GetChild(0).transform;
    }

    void Update()
    {
        if (player.GetComponent<Player>().playerState == Player.State.dead)
            return;
        
        // Sigue al jugador
        Vector3 lookAt = (transform.position - player.transform.position).normalized;
        Vector3 cPos = new Vector3(lookAt.x, 0, 0);
        transform.Translate(-cPos * (Time.deltaTime + moveSpeed) );

        // Movimiento oscilante
        oscilantMov.localPosition = new Vector3(
            xAmplitude * Mathf.Cos(Time.time * xFrequency),
            yAmplitude * Mathf.Sin(Time.time * yFrequency)
        );

        // Rango en que considera "bajo el jugador"
        onPlayer = Mathf.Abs(oscilantMov.position.x - player.transform.position.x) <= range;
        
        // Lanzamiento de bombas
        if (!bombAvailable)
        {
            if (fireRateCounter < bombFireRate)
                fireRateCounter += Time.deltaTime;
            else
                bombAvailable = true;
        }
        else
        {
            if (onPlayer)
            {
                StartCoroutine( OnPropulsion() );
                StartCoroutine( OnPropulsion(-1) );
            }

            bombAvailable = false;
            fireRateCounter = 0;
        }   
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

        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

        while (speedY >= -0.1f)
        {
            acceleration = force / mass;
            speedY += (gAccel + acceleration) * Time.deltaTime;
            bomb.transform.Translate(new Vector3(
                speedX * Time.deltaTime,
                speedY
            ));

            force = 0;
            bomb.GetComponent<Rigidbody>().AddForce(Vector3.down * 3.4f, ForceMode.Impulse);
            yield return null;
        }

        yield return new WaitForSeconds(2.8f);
        Destroy(bomb);
    }
}