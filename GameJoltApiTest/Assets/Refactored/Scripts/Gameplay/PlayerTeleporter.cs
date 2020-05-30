using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    private TerrainDefinitionVar terrainVar; 

    [SerializeField]
    private TransformVar submarine;

    [SerializeField]
    private Vector2Var submarinePos;

    private void Awake()
    {
        terrainVar.OnChange += TerrainChanged;
        submarinePos.OnChange += PosChanged;
    }

    private void OnDestroy()
    {
        terrainVar.OnChange -= TerrainChanged;
        submarinePos.OnChange -= PosChanged;
    }

    private void TerrainChanged(TerrainDefinition oldTerrain, TerrainDefinition newTerrain)
    {
        if(newTerrain == null || submarinePos.Value == null)
        {
            return;
        }

        PosChanged(Vector2.zero, submarinePos.Value);
    }
    private void PosChanged(Vector2 oldPos, Vector2 newPos)
    {
        if(terrainVar.Value == null)
        {
           return;
        }
        float leftAnchor = terrainVar.Value.MinX;
        float rightAnchor = terrainVar.Value.MaxX;

        if(newPos.x < leftAnchor)
        {
            float offset = Mathf.Min((leftAnchor - newPos.x),0);
            TeleportTo(rightAnchor + offset);
        } 
        else if(newPos.x >  rightAnchor)
        {
            float offset =  Mathf.Max((rightAnchor - newPos.x),0);
            TeleportTo(leftAnchor + offset);
        }
    }

    private void TeleportTo(float xPos)
    {
        submarine.Value.position = new Vector3(xPos,
                                               submarine.Value.position.y, 
                                               submarine.Value.position.z);
    }
}
