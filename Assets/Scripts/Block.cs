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
        Debug.Log("Choque contra un bloque destructible");
        StartCoroutine( Jumping() );
    }

    void OnSurpriseBlock()
    {
        Debug.Log("Choque contra un bloque sorpresa");
        StartCoroutine( Jumping() );
    }

    void OnIndestructibleBlock()
    {
        Debug.Log("Choque contra un bloque indestructible");
    }

    void OnStructBlock()
    {
        Debug.Log("Choque contra un bloque indestructible golpeado");
    }

    IEnumerator Jumping()
    {
        float mass = 1;
        float gravity = -6.8f;
        float force = 24;
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
    }
}
