using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BrickTypes {
        DestructibleBrickBlock,
        SurpriseBlock,
        IndestructibleBrickBlock
    };
    public BrickTypes brickType;
    private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeBrickState(GameObject player)
    {
        this.player = player;

        switch (brickType)
        {
            case BrickTypes.DestructibleBrickBlock:
                OnDestructibleBrickBlock();
                break;
            case BrickTypes.SurpriseBlock:
                OnSurpriseBlock();
                break;
            case BrickTypes.IndestructibleBrickBlock:
                OnIndestructibleBrickBlock();
                break;
        }
    }

    void OnDestructibleBrickBlock()
    {
        Debug.Log("Choque contra un bloque destructible");
    }

    void OnSurpriseBlock()
    {
        Debug.Log("Choque contra un bloque sorpresa");
    }

    void OnIndestructibleBrickBlock()
    {
        Debug.Log("Choque contra un bloque indestructible");
    }
}
