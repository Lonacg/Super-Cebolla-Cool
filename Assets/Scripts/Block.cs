using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockTypes {
        DestructibleBlock,
        IndestructibleBlock,
        SurpriseBlock
    };
    public BlockTypes blockType;
    private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeBlockState(GameObject player)
    {
        this.player = player;

        switch (blockType)
        {
            case BlockTypes.DestructibleBlock:
                OnDestructibleBlock();
                break;
            case BlockTypes.IndestructibleBlock:
                OnIndestructibleBlock();
                break;
            case BlockTypes.SurpriseBlock:
                OnSurpriseBlock();
                break;
        }
    }

    void OnDestructibleBlock()
    {
        Debug.Log("Choque contra un bloque destructible");
    }

    void OnSurpriseBlock()
    {
        Debug.Log("Choque contra un bloque sorpresa");
    }

    void OnIndestructibleBlock()
    {
        Debug.Log("Choque contra un bloque indestructible");
    }
}
